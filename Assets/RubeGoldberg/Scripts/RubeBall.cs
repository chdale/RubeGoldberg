using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubeBall : MonoBehaviour {

    public GameController GameController;

    private Vector3 startingPosition;
    
    private float velocityNudge = 1.1f; // still slows down from initial collision, this should make up for it
    private Vector3 prevVelocity;
    private Rigidbody thisRB;

    // Use this for initialization
    void Start () {
        startingPosition = transform.position;
        thisRB = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        prevVelocity = thisRB.velocity; //Get previous velocity
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Collectible", StringComparison.InvariantCultureIgnoreCase))
        {
            thisRB.velocity = prevVelocity * velocityNudge; //Makes up for loss of velocity on collectible
        }
        if(collision.gameObject.tag.Equals("Floor", StringComparison.InvariantCultureIgnoreCase))
        {
            Reset();
        }
    }

    public void Reset()
    {
        transform.position = startingPosition;
        thisRB.useGravity = true;
        thisRB.isKinematic = false;
        GameController.Reset();
    }
}
