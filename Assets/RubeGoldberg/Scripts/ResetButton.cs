using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour, IInteractable {
    public GameController gameController;

    public void Interact()
    {
        gameController.ball.GetComponent<RubeBall>().Reset();
        gameController.Reset();
    }
}
