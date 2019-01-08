using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubeFan : MonoBehaviour, IRubeObject {
    public float rubeForce = 2f;
    public FanBehaviour fanBehaviour;

    private GameObject rubeBall;
    private Rigidbody rubeBallRB;
    private float fanSpeed = 500f;
    // Use this for initialization
    void Start () {
        rubeBall = FindObjectOfType<RubeBall>().gameObject;
        rubeBallRB = rubeBall.GetComponent<Rigidbody>();
        fanBehaviour = FindObjectOfType<FanBehaviour>();
        if (fanBehaviour == null)
        {
            fanBehaviour = (FanBehaviour)ScriptableObject.CreateInstance(typeof(FanBehaviour));
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!fanBehaviour.FanBlow)
        {
            this.GetComponentInChildren<Transform>().Rotate(Vector3.forward * Time.deltaTime * fanSpeed);
        }
        else
        {
            this.GetComponentInChildren<Transform>().Rotate(Vector3.back * Time.deltaTime * fanSpeed);
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
        if(!fanBehaviour.FanBlow)
        {
            rb.AddForce(this.gameObject.transform.forward * rubeForce, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(-this.gameObject.transform.forward * rubeForce, ForceMode.Acceleration);
        }
    }
}
