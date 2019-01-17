using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text gameIntroText;

    private AudioSource musicSource;
    public MusicSelection musicSelection;

    // publicly accessible and settable
    public List<GameObject> collectibles;
    public GameObject goal;
    public GameObject ball;
    public List<GameObject> barParents;
    public List<GameObject> boundaries;
    public GameObject player;
    public OculusUI oculusUI;
    public bool isTutorial;
    public bool isOutsidePlatform = false;
    public TeleportInputController teleportController;
    public ObjectMenuController menuController;
    public GrabInputController[] grabControllers;

    // Used only in gameController
    public int collectiblesRemaining;

    private string[] sceneNames = new string[] { "Level0", "Level1", "Level2", "Level3", "Level4" };

    private void Awake()
    {
        var levelIndex = Array.IndexOf<string>(sceneNames, SceneManager.GetActiveScene().name);
        SetLevelIntro(levelIndex);
        if (levelIndex == 0)
        {
            isTutorial = true;
        }
        else
        {
            isTutorial = false;
        }
        musicSource = musicSelection.SelectMusic(isTutorial);
        musicSource.Play();
    }
    // Use this for initialization
    void Start () {
        collectiblesRemaining = collectibles.Count;
	}
	
	// Update is called once per frame
	void Update () {
        isOutsidePlatform = (-3.2f > player.transform.position.z) ||
                (.9f < player.transform.position.z) ||
                (-2.5 > player.transform.position.x) ||
                (1.7f < player.transform.position.x);

        if (isTutorial && teleportController.platformComplete && !menuController.menuComplete && !menuController.menuActive)
        {
            menuController.SetMenuReady();
        }

        if (isTutorial && menuController.itemCreationComplete && grabControllers.All(x => !x.grabComplete))
        {
            foreach (var grabController in grabControllers)
            {
                grabController.SetGrabReady();
            }
        }

        if ((-3.2f > ball.transform.position.z && -3.2f > player.transform.position.z) ||
                (.9f < ball.transform.position.z && .9f < player.transform.position.z) ||
                (-2.5 > ball.transform.position.x && -2.5 > player.transform.position.x) ||
                (1.7f < ball.transform.position.x && 1.7f < player.transform.position.x))
        {
            boundaries.ForEach(x => x.GetComponent<ChangeColor>().Cheater());
            goal.SetActive(false);
            oculusUI.SetReset(true);
        }
		if (collectiblesRemaining == 0)
        {
            Debug.Log("All stars collected!");
            goal.GetComponent<Goal>().Open();
        }
	}

    public void CollectibleCollected()
    {
        collectiblesRemaining--;
    }

    public void Reset()
    {
        collectiblesRemaining = collectibles.Count;
        goal.SetActive(true);
        goal.GetComponent<Goal>().Close();
        foreach (var collectible in collectibles)
        {
            collectible.GetComponent<Collectible>().Reset();
        }
        foreach (var barParent in barParents)
        {
            barParent.GetComponent<BarParent>().Reset();
        }
        foreach (var boundary in boundaries)
        {
            boundary.GetComponent<ChangeColor>().Reset();
        }
        oculusUI.SetReset(false);
    }

    public void GameWon()
    {
        StartCoroutine(NextLevel());
    }

    public void SetGrabComplete()
    {
        foreach (var grabController in  grabControllers)
        {
            grabController.SetGrabComplete();
        }
    }

    private IEnumerator NextLevel()
    {
        int sceneIndex;
        sceneIndex = Array.IndexOf<string>(sceneNames, SceneManager.GetActiveScene().name);

        yield return new WaitForSeconds(2f);

        if (sceneIndex < sceneNames.Length - 1)
        {
            SceneManager.LoadScene(sceneNames[sceneIndex + 1]);
        }
        else
        {
            SceneManager.LoadScene(sceneNames[0]);
        }
    }

    private void SetLevelIntro(int index)
    {
        if (index == 0)
        {
            gameIntroText.text = "Tutorial";
        }
        else
        {
            gameIntroText.text = "Level " + index;
        }

        StartCoroutine(SetLevelTextActive(gameIntroText.transform.parent.parent.parent.gameObject));
    }

    private IEnumerator SetLevelTextActive(GameObject obj)
    {

        obj.SetActive(true);
        yield return new WaitForSeconds(2f);
        obj.SetActive(false);
        teleportController.SetTutorialActive();
    }
}
