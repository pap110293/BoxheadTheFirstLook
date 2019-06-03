using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int mainSceneIndex = 1;
    public int optionSceneIndex = 2;
    public GameObject mainMenuPanel;
    public GameObject optionPanel;

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
        mainMenuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        mainMenuPanel.SetActive(true);
        optionPanel.SetActive(false);

    }
}
