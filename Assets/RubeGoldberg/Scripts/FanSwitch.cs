using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSwitch : MonoBehaviour, IRotatable {
    public AudioSource leverSource;
    public AudioClip leverClip;
    private bool leverGo = false;

    public FanBehaviour fanBehaviour;
    public GameObject forwardUI;
    public GameObject backwardUI;

    private float fanRotationCeiling = 12f;
    private float fanRotationThreshold = 0f;
    [SerializeField]
    private bool isGrabbed = false;
    [SerializeField]
    private bool isRotating = false;
    private GameObject playerHand;
    private Vector3 playerHandStartPos;
    [SerializeField]
    private bool rotateForwards;
    [SerializeField]
    private float switchSpeed = 15f;
	// Use this for initialization
	void Start ()
    {
        var previousFanRotation = WrapAngle(this.gameObject.transform.localRotation.eulerAngles.x);
        previousFanRotation = 12f;
        this.gameObject.transform.localEulerAngles = new Vector3(-previousFanRotation, 0f, 0f);

        fanBehaviour = FindObjectOfType<FanBehaviour>();
        if (fanBehaviour == null)
        {
            fanBehaviour = (FanBehaviour)ScriptableObject.CreateInstance(typeof(FanBehaviour));
        }
    }
	
	// Update is called once per frame
	void Update () {
        var wrappedAngle = WrapAngle(this.gameObject.transform.localRotation.eulerAngles.x);
        if (wrappedAngle > fanRotationThreshold)
        {
            fanBehaviour.fanState = FanState.Blow;
            if (forwardUI != null && backwardUI != null)
            {
                FanBlowUI(true);
            }
        }
        if (wrappedAngle < fanRotationThreshold)
        {
            fanBehaviour.fanState = FanState.Suck;
            if (forwardUI != null && backwardUI != null)
            {
                FanBlowUI(false);
            }
        }
        if (isGrabbed && !isRotating)
        {
            if (wrappedAngle < fanRotationThreshold && playerHand.transform.position.z > playerHandStartPos.z)
            {
                isRotating = true;
                rotateForwards = true;
            }
            if (wrappedAngle > fanRotationThreshold && playerHand.transform.position.z < playerHandStartPos.z)
            {
                isRotating = true;
                rotateForwards = false;
            }
        }
        if (isRotating)
        {
            SwitchRotate();
        }
	}

    private void SwitchRotate()
    {
        if (rotateForwards)
        {
            if (WrapAngle(this.gameObject.transform.localRotation.eulerAngles.x) >= fanRotationCeiling)
            {
                isRotating = false;
                leverGo = false;
            }
            else
            {
                if (!leverGo)
                {
                    leverSource.PlayOneShot(leverClip);
                }
                this.gameObject.transform.Rotate(transform.right * Time.deltaTime * switchSpeed);
                leverGo = true;
            }
        }
        else
        {
            if (WrapAngle(this.gameObject.transform.localRotation.eulerAngles.x) <= -fanRotationCeiling)
            {
                isRotating = false;
                leverGo = false;
            }
            else
            {
                if (!leverGo)
                {
                    leverSource.PlayOneShot(leverClip);
                }
                this.gameObject.transform.Rotate(-transform.right * Time.deltaTime * switchSpeed);
                leverGo = true;
            }
        }
    }

    public void SetGrabbed(bool grabbed, GameObject grabOrigin)
    {
        isGrabbed = grabbed;
        if (grabbed)
        {
            playerHand = grabOrigin;
            playerHandStartPos = grabOrigin.transform.position;
        }
    }

    //https://forum.unity.com/threads/solved-how-to-get-rotation-value-that-is-in-the-inspector.460310/
    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

    private static float UnwrapAngle(float angle)
    {
        if (angle >= 0)
            return angle;

        angle = -angle % 360;

        return 360 - angle;
    }

    public bool IsRotating()
    {
        return isRotating;
    }

    private void FanBlowUI(bool forward)
    {
        if (forward)
        {
            forwardUI.SetActive(true);
            backwardUI.SetActive(false);
        }
        else
        {
            forwardUI.SetActive(false);
            backwardUI.SetActive(true);
        }
    }
}
