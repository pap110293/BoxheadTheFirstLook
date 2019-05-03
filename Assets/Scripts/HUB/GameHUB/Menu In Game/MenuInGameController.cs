using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGameController : MonoBehaviour
{
    public int mainMenuSceneIndex = 0;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        MasterManager.menuInGameController = this;
        this.Hide();
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
                this.Show();
            }
        }
    }

    public void Show()
    {
        if (!MasterManager.isPause)
            return;

        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    public void ResumeGame()
    {
        MasterManager.ResumeGame();
        this.Hide();
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
}