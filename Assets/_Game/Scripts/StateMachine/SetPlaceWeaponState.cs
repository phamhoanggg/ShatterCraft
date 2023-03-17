using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlaceWeaponState : IState
{
    public void OnEnter()
    {
        GameManager.instance.DisplaySetablePlace();
    }

    public void OnExecute()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray myRay = GameManager.instance.MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(myRay, out RaycastHit hit, 100, GameManager.instance.WeaponPlaceLayer))
            {
                if (GameManager.instance.SelectedWeapon != PoolType.None)
                {
                    GameManager.instance.DisplayPlaced();
                    Cache.GetWeaponPlace(hit.collider).SetWeapon(GameManager.instance.SelectedWeapon);
                    
                    OnExit();
                }
            }
        }
    }

    public void OnExit() 
    {
        GameManager.instance.ResumeGame();
        GameManager.instance.SelectedWeapon = PoolType.None;
        GameManager.instance.ChangePhase(null);
    }
}
