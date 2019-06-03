using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInGameController : MonoBehaviour
{
    public int mainMenuSceneIndex = 0;
    public GameObject optionPanel;
    [Header("Pause panel")]
    public GameObject pausePanel;
    [Header("finish panel")]
    public GameObject finishPanel;
    public Text header;
    public Text score;

    private void Awake()
    {
        MasterManager.menuInGameController = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.HidePauseMenu();
        this.HideFinishPanel();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && MasterManager.gameController.isEndPlayProcess == false)
        {
            if (MasterManager.isPause)
            {
                this.ResumeGame();
                MasterManager.LockCursor();
            }
            else
            {
                MasterManager.PauseGame();
                MasterManager.UnLockCursor();
                this.ShowPauseMenu();
            }
        }
    }

    public void ShowPauseMenu()
    {
        if (!MasterManager.isPause)
            return;

        pausePanel.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pausePanel.SetActive(false);
    }

    public void ResumeGame()
    {
        MasterManager.ResumeGame();
        this.HidePauseMenu();
    }

    public void HideFinishPanel()
    {
        MasterManager.ResumeGame();
        MasterManager.LockCursor();
        finishPanel.SetActive(false);
    }

    public void ShowFinishPanel(string text)
    {
        header.text = text;
        this.score.text = MasterManager.scoreController.currentPoint + "";
        finishPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(mainMenuSceneIndex);
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        MasterManager.gameController.PlayAgain();
    }
}