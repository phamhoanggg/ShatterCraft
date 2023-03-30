using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeLog : MonoBehaviour
{
    [SerializeField] private Transform logTransform;

    private void FixedUpdate()
    {
        logTransform.eulerAngles += new Vector3(1, 0, 0); 
    }
}
