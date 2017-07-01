using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelTime : MonoBehaviour
{
    private Text text;


    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        float levelTime = Mathf.Clamp(GameManager.instance.LevelTime, 0, GameManager.instance.TotalTime);
        text.text = levelTime.ToString("0");

        if(levelTime <= GameManager.instance.TotalLevelTime/2.5)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
    }
}