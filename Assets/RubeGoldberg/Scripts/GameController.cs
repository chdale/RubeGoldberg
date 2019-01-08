using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    // publicly accessible and settable
    public List<GameObject> collectibles;
    public GameObject goal;
    public GameObject ball;
    public List<GameObject> barParents;
    public List<GameObject> boundaries;
    public GameObject player;
    public OculusUI oculusUI;

    // Used only in gameController
    public int collectiblesRemaining;

    private string[] sceneNames = new string[] { "Level1", "Level2", "Level3", "Level4" };
	// Use this for initialization
	void Start () {
        collectiblesRemaining = collectibles.Count;
	}
	
	// Update is called once per frame
	void Update () {
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
        int sceneIndex;
        sceneIndex = Array.IndexOf<string>(sceneNames, SceneManager.GetActiveScene().name);
        if (sceneIndex < sceneNames.Length - 1)
        {
            SceneManager.LoadScene(sceneNames[sceneIndex+1]);
        }
        else
        {
            SceneManager.LoadScene(sceneNames[0]);
        }
    }
}
