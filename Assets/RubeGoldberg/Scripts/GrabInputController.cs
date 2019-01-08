using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrabInputController : MonoBehaviour {

    public float throwForce = 3f;
    public OVRInput.Controller controller;

    private string[] throwableTags = { "RubeBall", "RubeObject" };
    private GameObject rotatingObject;

    private void Update()
    {
        if ((controller == OVRInput.Controller.LTouch && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger)) || (controller == OVRInput.Controller.RTouch && OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger)))
        {
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
                    ThrowObject(other);
                }
                else
                {
                    ReleaseObject(other);
                }
            }
            if ((controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)) || (controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)))
            {
                GrabObject(other);
            }
        }
        var rotatableObject = other.GetComponent<IRotatable>();
        if (rotatableObject != null)
        {
            if (!rotatableObject.IsRotating() && (controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)) || (controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)))
            {
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
    }

    void ThrowObject(Collider collider)
    {
        collider.transform.SetParent(null);
        Rigidbody rb = collider.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = OVRInput.GetLocalControllerVelocity(controller) * throwForce;
        rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);
    }

    void ReleaseObject(Collider collider)
    {
        collider.transform.SetParent(null);
    }
}
