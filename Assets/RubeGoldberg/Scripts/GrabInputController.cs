using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrabInputController : MonoBehaviour {

    public GameController gameController;
    public OculusUI oculusUI;
    public bool grabComplete = false;
    private bool grabActive = false;

    public AudioSource grabSource;
    public AudioClip grabClip;
    private bool grabGo = false;

    public float throwForce = 3f;
    public OVRInput.Controller controller;

    private string[] throwableTags = { "RubeBall", "RubeObject" };
    private GameObject rotatingObject;
    private GameObject rubeBall;

    private void Update()
    {
        if ((controller == OVRInput.Controller.LTouch && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger)) || (controller == OVRInput.Controller.RTouch && OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger)))
        {
            grabGo = false;
            if (rotatingObject != null)
            {
                rotatingObject.GetComponent<IRotatable>().SetGrabbed(false, this.gameObject);
                rotatingObject = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (throwableTags.Contains(other.gameObject.tag))
        {
            if ((controller == OVRInput.Controller.LTouch && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger)) || (controller == OVRInput.Controller.RTouch && OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger)))
            {
                if (other.gameObject.tag.Equals("RubeBall", StringComparison.InvariantCultureIgnoreCase))
                {
                    grabGo = false;
                    ThrowObject(other);
                }
                else
                {
                    grabGo = false;
                    ReleaseObject(other);
                }
            }
            if ((controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)) || (controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)))
            {
                if (!grabGo)
                {
                    grabSource.PlayOneShot(grabClip);
                    grabGo = true;
                }
                GrabObject(other);
            }
        }
        var rotatableObject = other.GetComponent<IRotatable>();
        if (rotatableObject != null)
        {
            if (!rotatableObject.IsRotating() && (controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)) || (controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)))
            {
                if (!grabGo)
                {
                    grabSource.PlayOneShot(grabClip);
                    grabGo = true;
                }
                rotatableObject.SetGrabbed(true, this.gameObject);
                rotatingObject = (rotatableObject as MonoBehaviour).gameObject;
            }
        }
        var interactableObject = other.GetComponent<IInteractable>();
        if (interactableObject != null)
        {
            interactableObject.Interact();
        }
    }

    void GrabObject(Collider collider)
    {
        collider.transform.SetParent(gameObject.transform);
        collider.GetComponent<Rigidbody>().isKinematic = true;
        if (collider.gameObject.tag.Equals("RubeBall", StringComparison.InvariantCultureIgnoreCase) && 
            gameController.isTutorial &&
            !grabComplete &&
            grabActive &&
            gameController.menuController.itemCreationComplete)
        {
            gameController.SetGrabComplete();
            StartCoroutine(SetObjectiveAndThrowUI());
        }
    }

    void ThrowObject(Collider collider)
    {
        collider.transform.SetParent(null);
        Rigidbody rb = collider.GetComponent<Rigidbody>();
        rubeBall = collider.gameObject;
        var rbScript = rubeBall.GetComponent<RubeBall>();
        rbScript.PlayThrowAudio();
        rb.isKinematic = false;
        rb.velocity = OVRInput.GetLocalControllerVelocity(controller) * throwForce;
        rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);
    }

    void ReleaseObject(Collider collider)
    {
        collider.transform.SetParent(null);
    }

    public void SetGrabReady()
    {
        oculusUI.SetGrab(true);
        grabActive = true;
    }

    public void SetGrabComplete()
    {
        grabComplete = true;
        oculusUI.SetGrab(false);
    }

    private IEnumerator SetObjectiveAndThrowUI()
    {
        oculusUI.SetObjective(true);
        yield return new WaitForSeconds(10f);
        oculusUI.SetObjective(false);
        oculusUI.SetThrow(true);
    }
}
