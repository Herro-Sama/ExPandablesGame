using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TimerBar : MonoBehaviour
{
    private Image image;

    [SerializeField]
    private Color col;

    [SerializeField]
    private Color col2;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        float fill =  GameManager.instance.LevelTime / GameManager.instance.TotalLevelTime;
        image.fillAmount = fill;

        if (fill <= 0.5f)
        {
            //image.color = Color.Lerp(col, col2, ((1 - (fill*2))));
            image.color = col;
        }
        else
        {
            //image.color = Color.Lerp(Color.yellow, Color.green, (fill*2)-1);
            image.color = col;
        }      
    }
}