﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void Retry()
    {
        GameManager.instance.StartNewGame();
    }
}
