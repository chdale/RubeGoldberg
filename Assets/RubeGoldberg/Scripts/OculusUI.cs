using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusUI : MonoBehaviour {

    public GameObject resetUI;

    public void SetReset(bool val)
    {
        resetUI.SetActive(val);
    }
}
