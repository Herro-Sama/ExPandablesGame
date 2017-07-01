using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimePool : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        float value = Mathf.Clamp(GameManager.instance.PoolTime, 0, GameManager.instance.TotalTime);
        text.text = value.ToString("0");
    }
}