using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    int slide = 0;

    private void Start()
    {
        if(GameManager.instance)
        {
            GameManager.instance.StopBackingMusic();
            Destroy(GameManager.instance.gameObject);
        }
    }
    private void Update()
    {
        if(Input.anyKeyDown)
        {
            Slider();
        }
    }

    private void Slider()
    {
        slide++;

        switch(slide)
        {
            default:
            case 1:
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                break;
        }
    }
}
