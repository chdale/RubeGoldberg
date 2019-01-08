using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    private Color startColor;
    private Renderer renderer;

    private void Start()
    {
        renderer = this.gameObject.GetComponent<Renderer>();
        startColor = renderer.material.color;
    }

    public void Cheater()
    {
        renderer.material.color = new Color(.255f, 0f, 0f, .90f);
    }

    public void Reset()
    {
        renderer.material.color = startColor;
    }
}
