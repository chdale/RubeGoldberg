using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeleportInputController : MonoBehaviour {
    public AudioClip teleportationHold;
    private bool holdPlay = false;
    public AudioClip teleportationGo;
    private bool goPlay = false;
    public AudioSource teleportationHoldSource;
    public AudioSource teleportationGoSource;
    public GameController gameController;
    public OculusUI oculusUI;
    private bool teleportInstructionActive = false;
    private bool platformInstructionActive = false;
    public bool platformComplete = false;
    public GameObject teleportArrow;

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
            goPlay = false;
            if (!holdPlay)
            {
                teleportationHoldSource.Play();
                holdPlay = true;
            }
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
                holdPlay = false;
                teleportationHoldSource.Stop();
                if (!goPlay)
                {
                    teleportationGoSource.PlayOneShot(teleportationGo);
                    goPlay = true;
                }
                teleportAimerObject.SetActive(false);
                player.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudgeAmount, teleportLocation.z);
                if (teleportInstructionActive)
                {
                    oculusUI.SetTeleport(false);
                    oculusUI.SetPlatform(true);
                    teleportArrow.SetActive(true);
                    platformInstructionActive = true;
                    teleportInstructionActive = false;
                }
                if (platformInstructionActive && !gameController.isOutsidePlatform)
                {
                    oculusUI.SetPlatform(false);
                    teleportArrow.SetActive(false);
                    platformInstructionActive = false;
                    platformComplete = true;
                }
            }
        }
	}

    public void SetTutorialActive()
    {
        if (gameController.isTutorial)
        {
            teleportInstructionActive = true;
            oculusUI.SetTeleport(true);
        }
    }
}
