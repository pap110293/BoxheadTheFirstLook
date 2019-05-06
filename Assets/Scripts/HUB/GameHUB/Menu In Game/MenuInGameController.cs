using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGameController : MonoBehaviour
{
    public int mainMenuSceneIndex = 0;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject losePanel;

    private void Awake() {
        MasterManager.menuInGameController = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.HidePauseMenu();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void HideLosePanel()
    {
        losePanel.SetActive(false);
    }

    public void HideWinPanel()
    {
        winPanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
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
        
    }
}