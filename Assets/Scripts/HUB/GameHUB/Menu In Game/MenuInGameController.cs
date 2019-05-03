using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInGameController : MonoBehaviour
{
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
                MasterManager.ResumeGame();
                this.Hide();
            }
            else
            {
                MasterManager.PauseGame();
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
}