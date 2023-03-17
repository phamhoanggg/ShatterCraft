using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSaw : Weapon
{
    [SerializeField] private Transform sawTf;
    
    // Update is called once per frame
    void Update()
    {
        TF.rotation = Quaternion.Euler(0, Time.time * ATKSpeed, 0);
        sawTf.eulerAngles += new Vector3(0, 15, 0);
    }
}
