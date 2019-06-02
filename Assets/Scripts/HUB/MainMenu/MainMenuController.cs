using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int mainSceneIndex = 1;
    public int optionSceneIndex = 2;

    private void Start()
    {
        Time.timeScale = 1f;
        MasterManager.UnLockCursor();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(mainSceneIndex);
    }

    public void GotoOpstionScene()
    {
        //SceneManager.LoadScene(optionSceneIndex);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
