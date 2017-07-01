using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelTitle : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        text.text = (GameManager.instance.CurrentLevel + 1).ToString("0") + " / " + (GameManager.instance.TotalLevels).ToString("0");
    }
}