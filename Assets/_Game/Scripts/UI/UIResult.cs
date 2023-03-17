using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResult : UICanvas
{
    public void ClickNextLevel()
    {
        LevelController.instance.NextLevel();
        UI_Manager.instance.CloseUI<UIResult>();
    }
}
