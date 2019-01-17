using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {
    public AudioSource trampolineSoundSource;
    public AudioClip trampolineSoundClip;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("RubeBall", StringComparison.InvariantCultureIgnoreCase))
        {
            trampolineSoundSource.PlayOneShot(trampolineSoundClip);
        }
    }
}
