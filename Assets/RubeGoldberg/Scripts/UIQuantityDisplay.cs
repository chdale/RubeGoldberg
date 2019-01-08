using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuantityDisplay : MonoBehaviour {
    private ObjectMenuManager menuManager;
    [SerializeField]
    private int currentQty;
    private int previousQty;
    private int cap;
    private Text text;
	// Use this for initialization
	void Start () {
        menuManager = transform.parent.GetComponentInParent<ObjectMenuManager>();
        cap = menuManager.objectGenerationLimitList[menuManager.currentObject];
        currentQty = cap;
        foreach (Transform child in transform.GetChild(0).transform)
        {
            if (child.name.Equals("Quantity", StringComparison.InvariantCultureIgnoreCase))
            {
                text = child.GetComponent<Text>();
                AssignValue();
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        previousQty = currentQty;
        currentQty = menuManager.objectGenerationLimitList[menuManager.currentObject];
        if (currentQty != previousQty)
        {
            AssignValue();
        }
    }

    private void AssignValue()
    {
        if (text != null)
        {
            text.text = string.Format("{0} / {1}", currentQty, cap);
        }
    }
}
