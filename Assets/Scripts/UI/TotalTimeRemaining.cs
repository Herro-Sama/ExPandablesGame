﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TotalTimeRemaining : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        text.text = "/" + GameManager.instance.TotalTime.ToString("0");
    }
}