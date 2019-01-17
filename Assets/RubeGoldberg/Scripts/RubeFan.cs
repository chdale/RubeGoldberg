using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubeFan : MonoBehaviour, IRubeObject {
    public AudioSource suckSource;
    public AudioClip suckClip;
    public AudioSource blowSource;
    public AudioClip blowClip;

    public float rubeForce = 2f;
    public FanBehaviour fanBehaviour;

    private FanState previousFanState;
    private GameObject rubeBall;
    private Rigidbody rubeBallRB;
    private float fanSpeed = 500f;
    // Use this for initialization
    void Start() {
        rubeBall = FindObjectOfType<RubeBall>().gameObject;
        rubeBallRB = rubeBall.GetComponent<Rigidbody>();
        fanBehaviour = FindObjectOfType<FanBehaviour>();
        if (fanBehaviour == null)
        {
            fanBehaviour = (FanBehaviour)ScriptableObject.CreateInstance(typeof(FanBehaviour));
        }
        previousFanState = fanBehaviour.fanState;
        if (fanBehaviour.fanState == FanState.Suck)
        {
            suckSource.Play();
        }
        else
        {
            blowSource.Play();
        }
    }

    // Update is called once per frame
    void Update() {
        if (fanBehaviour.fanState == FanState.Suck)
        {
            if (previousFanState == FanState.Blow)
            {
                blowSource.Stop();
                suckSource.Play();
            }
            this.GetComponentInChildren<Transform>().Rotate(Vector3.forward * Time.deltaTime * fanSpeed);
            previousFanState = fanBehaviour.fanState;
        }
        else
        {
            if (previousFanState == FanState.Suck)
            {
                suckSource.Stop();
                blowSource.Play();
            }
            this.GetComponentInChildren<Transform>().Rotate(Vector3.back * Time.deltaTime * fanSpeed);
            previousFanState = fanBehaviour.fanState;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == rubeBall)
        {
            ApplyRubeForce(rubeBallRB);
        }
    }

    public void ApplyRubeForce(Rigidbody rb)
    {
        if (fanBehaviour.fanState == FanState.Suck)
        {
            rb.AddForce(this.gameObject.transform.forward * rubeForce, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(-this.gameObject.transform.forward * rubeForce, ForceMode.Acceleration);
        }
    }
       
}
