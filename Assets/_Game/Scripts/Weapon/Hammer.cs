using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    [SerializeField] private Animator hammerAnim;
    // Update is called once per frame
    void Update()
    {
        hammerAnim.speed = GameManager.instance.IsState(Enums.GameState.Playing) ? ATKSpeed : 0;
    }
}
