﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Behaviours")]
public class FanBehaviour : ScriptableObject {
    public FanState fanState = FanState.Suck;
}
