using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePickupScreen : MonoBehaviour
{
    [SerializeField]
    private Text header;

    private float time;

    public void Activate(float numberOfSeconds)
    {
        time = numberOfSeconds;
        GameManager.instance.StopCounter();
        header.text = "You've Got " + numberOfSeconds.ToString("0") + " Seconds!";
    }

    public void Take()
    {
        GameManager.instance.TakeTimeToLevel(time);
        GameManager.instance.RestartCounter();
        Destroy(gameObject);
    }

    public void Give()
    {
        GameManager.instance.GiveTimeToPool(time);
        GameManager.instance.RestartCounter();
        Destroy(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Take();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Give();
        }

    }
}