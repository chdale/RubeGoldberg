using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubeMetalPlank : MonoBehaviour, IRubeObject {
    public float rubeForce = 1f;

    private GameObject rubeBall;
    private Rigidbody rubeBallRB;
	// Use this for initialization
	void Start () {
        rubeBall = FindObjectOfType<RubeBall>().gameObject;
        rubeBallRB = rubeBall.GetComponent<Rigidbody>();
	}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == rubeBall)
        {
            ApplyRubeForce(rubeBallRB);
        }
    }

    public void ApplyRubeForce(Rigidbody rb)
    {
        rb.AddForce(this.gameObject.transform.right * rubeForce, ForceMode.Acceleration);
    }
}
