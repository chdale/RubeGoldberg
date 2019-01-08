using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDirection : MonoBehaviour {
    public bool adjust;
    private Camera camera;
	// Use this for initialization
	void Start () {
        camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(camera.transform);
        if (adjust)
        {
            transform.Rotate(new Vector3(0f, 180f, 0f));
        }
	}
}
