using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSelection : MonoBehaviour
{
    [SerializeField]
    private Button startLevelButton;

    [SerializeField]
    private AudioClip drain;

    [SerializeField]
    private AudioClip gain;

    bool lower = false;
    AudioSource src;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }

   public void StartDraining(bool addingTimeToLevel)
   {
        GameManager.instance.StartDrainingTime(addingTimeToLevel);

        lower = false;
        src.volume = 0.32f;
        if (addingTimeToLevel)
        {
            src.clip = drain;
        }
        else
        {
            src.clip = gain;
        }
        src.Play();
   }

    public void StopDraining()
    {
        GameManager.instance.StopDrainingTime();
        lower = true;
    }

    public void StartLevel()
    {
        GameManager.instance.StartLevel();
    }

    private void Update()
    {
        if(GameManager.instance.LevelTime <= 0)
        {
            startLevelButton.interactable = false;
        }
        else
        {
            startLevelButton.interactable = true;
        }

        if(lower)
        {
            src.volume -= Time.deltaTime;
            if(src.volume <= 0)
            {
                lower = false;
            }
        }
    }
}