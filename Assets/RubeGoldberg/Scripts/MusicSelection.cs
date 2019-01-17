using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelection : MonoBehaviour {
    public AudioSource tutorialMusic;
    public AudioSource levelMusic;

    public AudioSource SelectMusic(bool isTutorial)
    {
        if (isTutorial)
        {
            return tutorialMusic;
        }
        else
        {
            return levelMusic;
        }
    }
}
