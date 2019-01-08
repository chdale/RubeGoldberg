using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubeTarBar : MonoBehaviour, IRubeObject {

    private GameObject rubeBall;
    private Rigidbody rubeBallRB;
    // Use this for initialization
    void Start()
    {
        rubeBall = FindObjectOfType<RubeBall>().gameObject;
        rubeBallRB = rubeBall.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == rubeBall)
        {
            ApplyRubeForce(rubeBallRB);
        }
    }

    public void ApplyRubeForce(Rigidbody rb)
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        rubeBall.transform.parent = this.gameObject.transform.parent;
        GetComponentInParent<BarParent>().Rotate();
    }

    public void ReleaseBall()
    {
        rubeBall.transform.parent = this.gameObject.transform.parent.parent.parent;
        rubeBallRB.isKinematic = false;
        rubeBallRB.useGravity = true;
    }
}
