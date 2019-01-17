using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusUI : MonoBehaviour {

    public GameObject resetUI;
    public GameObject congratulationsUI;
    public GameObject teleportUI;
    public GameObject platformUI;
    public GameObject menuUI;
    public GameObject itemCreationUI;
    public GameObject grabUI;
    public GameObject throwUI;
    public GameObject objectiveUI;

    public void SetReset(bool val)
    {
        resetUI.SetActive(val);
    }

    public void SetCongratulations(bool val)
    {
        congratulationsUI.SetActive(val);
    }

    public void SetTeleport(bool val)
    {
        teleportUI.SetActive(val);
    }

    public void SetPlatform(bool val)
    {
        platformUI.SetActive(val);
    }

    public void SetMenu(bool val)
    {
        menuUI.SetActive(val);
    }

    public void SetItemCreation(bool val)
    {
        itemCreationUI.SetActive(val);
    }

    public void SetGrab(bool val)
    {
        grabUI.SetActive(val);
    }

    public void SetThrow(bool val)
    {
        throwUI.SetActive(val);
    }

    public void SetObjective(bool val)
    {
        objectiveUI.SetActive(val);
    }
}
