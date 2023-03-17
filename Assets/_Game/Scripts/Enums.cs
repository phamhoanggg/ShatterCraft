using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums
{
    public enum GameState
    {
        ChoosingWeapon,
        Playing,
        Result,
    }

    public enum UpgradeType
    {
        None,
        ADD,
        SIZE,
        SPEED,
        RANGE,
    }
}
