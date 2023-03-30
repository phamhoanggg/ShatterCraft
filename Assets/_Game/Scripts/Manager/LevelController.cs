using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : FastSingleton<LevelController>
{
    [SerializeField] private Level[] listLevel;
    public Level CurrentLevel;
    public int curLevelIdx;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        curLevelIdx = 0;
        NextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentLevel.LevelProgressValue >= CurrentLevel.LevelMaxValue)
        {
            EndLevel();
        }

        if (CurrentLevel.WeaponProgressValue >= CurrentLevel.WeaponMaxValue)
        {
            GameManager.instance.ChangePhase(GameManager.instance.SelectWeaponPhase);
            CurrentLevel.WeaponProgressValue = 0;
        }
    }

    public void StartLevel()
    {
        GameManager.instance.SetCoin(200);
        GameManager.instance.ChangeState(Enums.GameState.ChoosingWeapon);
        GameManager.instance.ChangePhase(GameManager.instance.SelectWeaponPhase);
        UI_Manager.instance.OpenUI<UIGamePlay>();
        CurrentLevel.OnInit();
    }

    public void EndLevel()
    {
        GameManager.instance.ChangeState(Enums.GameState.Result);
        GameManager.instance.ChangePhase(null);

        UI_Manager.instance.CloseUI<UIGamePlay>();
        UI_Manager.instance.CloseUI<UISelectUpgrade>();
        UI_Manager.instance.OpenUI<UIResult>();
    }

    public void NextLevel()
    {
        SimplePool.CollectAll();
        if (CurrentLevel != null)
        {
            CurrentLevel.gameObject.SetActive(false);
        }
        curLevelIdx++;
        CurrentLevel = Instantiate(listLevel[curLevelIdx - 1]);
        StartLevel();
    }

    public void RePlayLevel()
    {
        SimplePool.CollectAll();
        for (int i = 0; i < CurrentLevel.WeaponPlacesList.Length; i++)
        {
            CurrentLevel.WeaponPlacesList[i].IsPlaced = false;
        }
        CurrentLevel.gameObject.SetActive(false);
        StartLevel();
    }

    
}
