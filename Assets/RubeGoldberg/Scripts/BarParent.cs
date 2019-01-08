using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarParent : MonoBehaviour {
    public GameController gameController;
    public float rotateSpeed = 60f;

    private GameObject hinge;
    private bool rotating = false;
    private Quaternion startingRotation;
	// Use this for initialization
	void Start () {
        hinge = this.gameObject.transform.parent.Find("Hinge").gameObject;
        startingRotation = this.gameObject.transform.localRotation;
        gameController = FindObjectOfType<GameController>();
        gameController.barParents.Add(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (rotating)
        {
            this.gameObject.transform.RotateAround(hinge.transform.position, -transform.up, rotateSpeed * Time.deltaTime);
        }
    }

    public void Rotate()
    {
        rotating = true;
    }

    public void FinishRotation()
    {
        rotating = false;
        GetComponentInChildren<RubeTarBar>().ReleaseBall();
    }

    public void Reset()
    {
        rotating = false;
        this.gameObject.transform.localRotation = startingRotation;
    }
}
