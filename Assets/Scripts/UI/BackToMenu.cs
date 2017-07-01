using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    [SerializeField]
    private int level = 0;

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }
}
