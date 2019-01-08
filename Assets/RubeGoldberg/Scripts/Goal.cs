using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public GameController GameController;

    private Behaviour halo;
	// Use this for initialization
	void Start () {
        halo = (Behaviour)GetComponent("Halo");
	}

    public void Open()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        halo.enabled = true;
    }

    public void Close()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        halo.enabled = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("RubeBall", StringComparison.InvariantCultureIgnoreCase))
        {
            GameController.GameWon();
        }
    }
}
