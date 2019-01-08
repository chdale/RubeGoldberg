using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    public GameController GameController;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("RubeBall", StringComparison.InvariantCultureIgnoreCase))
        {
            Collected();
        }
    }

    public void Collected()
    {
        this.gameObject.SetActive(false);
        GameController.CollectibleCollected();
    }

    public void Reset()
    {
        this.gameObject.SetActive(true);
    }
}
