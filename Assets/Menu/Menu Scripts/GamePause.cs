using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject pauseMenu, pauseButton;

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Remuse()
    {
        Time.timeScale = 1f;
    }
}
