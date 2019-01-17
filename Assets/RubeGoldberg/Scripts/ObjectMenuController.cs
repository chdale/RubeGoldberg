using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuController : MonoBehaviour {
    public GameController gameController;
    public OculusUI oculusUI;

    public AudioSource menuCancelSource;
    public AudioClip menuCancelClip;
    public AudioSource menuSwitchSource;
    public AudioClip menuSwitchClip;
    public AudioSource createSource;
    public AudioClip createClip;

    public ObjectMenuManager objectMenuManager;
    public float creationCooldown = 3.0f;

    public bool menuComplete = false;
    public bool menuActive = false;
    public bool itemCreationComplete = false;

    private bool menuCycled = false;
    [SerializeField]
    private float menuResetThreshold = .35f;
    [SerializeField]
    private float menuScrollThreshold = .7f;
    [SerializeField]
    private bool creationAvailable = true;
    // Use this for initialization
    void Start () {
        objectMenuManager = GetComponentInChildren<ObjectMenuManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            if (objectMenuManager.isShowing)
            {
                menuCancelSource.PlayOneShot(menuCancelClip);
                objectMenuManager.Show(false);
            }
        }
        float menuStickX = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;
        if (menuStickX < menuResetThreshold && menuStickX > -menuResetThreshold)
        {
            menuCycled = false;
        }

        if (!menuCycled)
        {
            if (menuComplete && !itemCreationComplete)
            {
                SetCreationReady();
            }
            if (menuStickX > menuScrollThreshold)
            {
                if (gameController.isTutorial)
                {
                    oculusUI.SetMenu(false);
                    menuActive = false;
                    menuComplete = true;
                }
                menuCycled = true;
                objectMenuManager.MenuRight();
                menuSwitchSource.PlayOneShot(menuSwitchClip);
            }
            if (menuStickX < -menuScrollThreshold)
            {
                if (gameController.isTutorial)
                {
                    oculusUI.SetMenu(false);
                    menuActive = false;
                    menuComplete = true;
                }
                menuCycled = true;
                objectMenuManager.MenuLeft();
                menuSwitchSource.PlayOneShot(menuSwitchClip);
            }
        }

        if (creationAvailable && objectMenuManager.isShowing && OVRInput.Get(OVRInput.Button.SecondaryThumbstick))
        {
            if (gameController.isTutorial && menuComplete)
            {
                oculusUI.SetItemCreation(false);
                itemCreationComplete = true;
            }
            creationAvailable = false;
            objectMenuManager.Create();
            createSource.PlayOneShot(createClip);
            StartCoroutine(CreationCooldown(creationCooldown));
            objectMenuManager.Show(false);
        }
    }

    public void SetMenuReady()
    {
        oculusUI.SetMenu(true);
        menuActive = true;
    }

    public void SetCreationReady()
    {
        oculusUI.SetItemCreation(true);
    }

    private IEnumerator CreationCooldown(float creationCooldown)
    {
        yield return new WaitForSeconds(creationCooldown);
        creationAvailable = true;
    }
}
