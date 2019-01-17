using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    public AudioSource goalSource;
    public AudioClip goalClip;
    public GameController GameController;
    public OculusUI oculusUI;

    private GameObject rubeBall;
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
            goalSource.PlayOneShot(goalClip);
            if (!GameController.isTutorial)
            {
                oculusUI.SetCongratulations(true);
            }
            rubeBall = collider.gameObject;
            var rubeBallRB = rubeBall.GetComponent<Rigidbody>();
            rubeBallRB.velocity = new Vector3(0, 0, 0);
            rubeBallRB.useGravity = false;
            GameController.GameWon();
        }
    }
}
