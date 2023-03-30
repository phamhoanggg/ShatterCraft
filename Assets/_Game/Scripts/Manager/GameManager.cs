using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : FastSingleton<GameManager>
{
    [SerializeField] private Weapon[] weaponList;
    
    private Enums.GameState gameState;
    private IState gamePhase;

    public IState SelectWeaponPhase = new SelectWeaponState();
    public IState SetPlaceWeaponPhase = new SetPlaceWeaponState();

    
    public List<UpgradeTypeConfig> UpgradeList = new List<UpgradeTypeConfig>();
    
    public PoolType SelectedWeapon = PoolType.None;
    public Upgrade SelectedUpgrade;

    public Camera MainCamera;
    public LayerMask WeaponPlaceLayer, ZombieLayer;

    public float CoinAmount { get; private set; }

    public UIGamePlay GamePlayObject;

    public bool IsVibrate;

    protected override void Awake()
    {
        base.Awake();
        IsVibrate = true;
        GamePlayObject = UI_Manager.instance.OpenUI<UIGamePlay>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gamePhase != null)
        {
            gamePhase.OnExecute();
        }

        if (gameState == Enums.GameState.Playing && Input.GetMouseButtonDown(0))
        {
            Ray myRay = MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myRay, out RaycastHit hit, 100, ZombieLayer))
            {   
                Cache.GetZombie(hit.collider).OnHit(500);
            }
        }
    }

    public void DisplaySetablePlace()
    {
        WeaponPlace[] weaponPlaces = LevelController.instance.CurrentLevel.WeaponPlacesList;
        for (int i = 0; i < weaponPlaces.Length; i++)
        {
            if (weaponPlaces[i].IsPlaced == false)
            {
                weaponPlaces[i].DisplayPlaceable();
            }
        }
    }
    public void DisplayPlaced()
    {
        WeaponPlace[] weaponPlaces = LevelController.instance.CurrentLevel.WeaponPlacesList;
        for (int i = 0; i < weaponPlaces.Length; i++)
        {
            weaponPlaces[i].DisplayPlaced();
        }
    }

    public void ChangePhase(IState phase)
    {
        gamePhase = phase;

        if (gamePhase != null)
        {
            gamePhase.OnEnter();
        }   
    }

    public void ChangeState(Enums.GameState state)
    {
        gameState = state;
    }

    public bool IsState(Enums.GameState state)
    {
        return gameState == state;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameState = Enums.GameState.Playing;
    }

    public UpgradeTypeConfig[] GetUpgrade()
    {
        UpgradeTypeConfig[] upgradeLst = new UpgradeTypeConfig[3];
        List<UpgradeTypeConfig> availUpgrades = new List<UpgradeTypeConfig>();

        WeaponPlace[] weaponPlaces = LevelController.instance.CurrentLevel.WeaponPlacesList;
        // Check if there is available place for new weapon
        bool avail = false;
        for (int i = 0; i < weaponPlaces.Length; i++)
        {
            if (!weaponPlaces[i].IsPlaced)
            {
                avail = true;
                break;
            }
        }

        // If there is available place => add upgrade type ADD into list
        if (avail)
        {
            for (int i = 0; i < UpgradeList.Count; i++)
            {
                if (UpgradeList[i].Type == Enums.UpgradeType.ADD)
                {
                    availUpgrades.Add(UpgradeList[i]);
                }
            }
        }

        // Get all valid upgrade of all weapon placed
        for (int i = 0; i < weaponPlaces.Length; i++)
        {
            if (weaponPlaces[i].IsPlaced)
            {
                Weapon weapon = weaponPlaces[i].WeaponPlaced;
                for (int j = 0; j < UpgradeList.Count; j++)
                {
                    if (weapon.poolType == UpgradeList[j].WeaponType && UpgradeList[j].Type != Enums.UpgradeType.ADD)
                    {
                        availUpgrades.Add(UpgradeList[j]);
                    }
                }
            }
        }

        // Get random 3 upgrade from list
        for (int i = 0; i < 3; i++)
        {
            int j = Random.Range(0, availUpgrades.Count);
            upgradeLst[i] = availUpgrades[j];
            availUpgrades.RemoveAt(j);
        }
        return upgradeLst;
    }

    public void SetCoin(float value)
    {
        CoinAmount = value;
        GamePlayObject.CoinText.text = CoinAmount.ToString();
    }
}
