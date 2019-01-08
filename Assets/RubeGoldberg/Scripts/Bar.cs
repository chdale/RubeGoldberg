using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.name.Contains("Wood_Plank"))
        {
            this.GetComponentInParent<BarParent>().FinishRotation();
        }
    }
}
