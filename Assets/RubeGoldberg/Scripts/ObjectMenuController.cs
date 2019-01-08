using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuController : MonoBehaviour {
    public ObjectMenuManager objectMenuManager;
    public float creationCooldown = 3.0f;

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
	void Update ()
    {
        float menuStickX = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;
        if (menuStickX < menuResetThreshold && menuStickX > -menuResetThreshold)
        {
            menuCycled = false;
        }

        if (!menuCycled)
        {
            if (menuStickX > menuScrollThreshold)
            {
                menuCycled = true;
                objectMenuManager.MenuRight();
            }
            if (menuStickX < -menuScrollThreshold)
            {
                menuCycled = true;
                objectMenuManager.MenuLeft();
            }
        }

        if (creationAvailable && OVRInput.Get(OVRInput.Button.SecondaryThumbstick))
        {
            creationAvailable = false;
            objectMenuManager.Create();
            StartCoroutine(CreationCooldown(creationCooldown));
        }
    }

    private IEnumerator CreationCooldown(float creationCooldown)
    {
        yield return new WaitForSeconds(creationCooldown);
        creationAvailable = true;
    }
}
