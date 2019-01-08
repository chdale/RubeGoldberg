using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeleportInputController : MonoBehaviour {

    public int laserMask;
    public GameObject teleportAimerObject;
    public Vector3 teleportLocation;
    public GameObject player;
    
    private LineRenderer laser;
    private bool laserActive = false;
    private float yNudgeAmount;

	// Use this for initialization
	void Start () {
        yNudgeAmount = player.transform.position.y;
        laser = GetComponentInChildren<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > .5)
        {
            laser.gameObject.SetActive(true);
            laser.SetPosition(0, gameObject.transform.position);

            var hits = Physics.RaycastAll(transform.position, transform.forward, 15);
            var hitRay = hits.FirstOrDefault(x => x.transform.gameObject.layer == laserMask);
            if (hitRay.transform != null)
            {
                laserActive = true;
                teleportLocation = hitRay.point;
                laser.SetPosition(1, teleportLocation);
                teleportAimerObject.SetActive(true);
                teleportAimerObject.transform.position = new Vector3(teleportLocation.x, teleportLocation.y, teleportLocation.z);
            }
            else
            {
                teleportLocation = new Vector3(transform.forward.x * 15 + transform.position.x, transform.forward.y * 15 + transform.forward.y, transform.forward.z * 15 + transform.forward.z);
                var groundHits = Physics.RaycastAll(teleportLocation, -Vector3.up, 17, laserMask);
                hitRay = groundHits.FirstOrDefault(x => x.transform.gameObject.layer == laserMask);
                if (hitRay.transform != null)
                {
                    teleportLocation = new Vector3(transform.forward.x * 15 + transform.forward.x, hitRay.point.y, transform.forward.z * 15 + transform.forward.z);
                }
                else
                {
                    laserActive = false;
                    teleportAimerObject.SetActive(false);
                }
                laser.SetPosition(1, transform.forward * 15 + transform.position);
                teleportAimerObject.transform.position = teleportLocation;
            }
        }
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y < .5)
        {
            laser.gameObject.SetActive(false);
            if (laserActive)
            {
                teleportAimerObject.SetActive(false);
                player.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudgeAmount, teleportLocation.z);
            }
        }
	}
}
