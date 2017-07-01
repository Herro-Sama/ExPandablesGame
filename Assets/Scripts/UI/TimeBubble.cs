using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TimeBubble : MonoBehaviour
{
    [SerializeField]
    private bool pool = true;

    [SerializeField]
    private float scaleFactor = 150f;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.fillAmount = (pool ? GameManager.instance.PoolTime : GameManager.instance.LevelTime) / GameManager.instance.TotalTime;
        /*
        image.rectTransform.localScale =
            new Vector3(
                (pool ? GameManager.instance.PoolTime : GameManager.instance.LevelTime) / scaleFactor,
                (pool ? GameManager.instance.PoolTime : GameManager.instance.LevelTime) / scaleFactor,
                (pool ? GameManager.instance.PoolTime : GameManager.instance.LevelTime) / scaleFactor
        );
        */
    }
}