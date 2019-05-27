using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int PrepareTime = 5;

    private PlayerLife playerLife;
    public bool isDone = false;

    void Start()
    {
        MasterManager.gameController = this;
        StartNewGame();
    }

    private IEnumerator StartNewWaveProcess()
    {
        MasterManager.spawEnemyManager.totalEnemy = 0;
        var levelData = MasterManager.levelConfigManager.GetNextLevelData();
        if (levelData != null)
        {
            yield return StartCoroutine(LoadLevel(levelData));
            isDone = false;
        }
        else
        {
            isDone = true;
        }
    }

    IEnumerator LoadLevel(LevelData level)
    {
        if (level == null) yield return null;
        if (MasterManager.spawEnemyManager)
        {
            MasterManager.spawEnemyManager.InitSpawners(level);
            MasterManager.itemSpawnerController.InitSpawners(level);
        }
        for (int i = 0; i < PrepareTime; i++)
        {
            PushNotification("START NEXT WAVE IN : " + (PrepareTime - i));
            yield return new WaitForSeconds(1f);
        }

        PushNotification("START WAVE " + (MasterManager.levelConfigManager.CurrentLevel));

        if (MasterManager.spawEnemyManager)
        {
            MasterManager.spawEnemyManager.StartAll();
        }
    }

    IEnumerator GameProcess()
    {
        yield return StartCoroutine(StartNewWaveProcess());

        if (isDone == false)
        {
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(Playing());

            if (CheckIsLose())
            {
                StartCoroutine(LosingProcess());
            }
            else if (CheckIsWinTheWave())
            {
                StartCoroutine(WiningProcess());
            }
        }
        else
        {
            StartCoroutine(WinAllWaveProcess());
        }
    }

    private IEnumerator StartNewGameProcess()
    {
        MasterManager.levelConfigManager.Restart();
        MasterManager.scoreController.ResetScore();
        yield return StartCoroutine(GameProcess());
    }

    private IEnumerator WinAllWaveProcess()
    {

        PushNotification("YOU WIN ALL THE WAVE");
        ShowFinishPanel();
        yield return null;
    }

    private IEnumerator Playing()
    {
        while (CheckIsLose() == false && CheckIsWinTheWave() == false)
        {
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator LosingProcess()
    {
        yield return new WaitForSeconds(PrepareTime);
        ShowFinishPanel();
        yield return null;
    }

    private void ShowFinishPanel()
    {
        MasterManager.menuInGameController.ShowFinishPanel(100, "Your score");
        MasterManager.PauseGame();
        MasterManager.UnLockCursor();
    }

    IEnumerator WiningProcess()
    {
        PushNotification("+++++ CLEAR +++++");
        StartCoroutine(GameProcess());
        yield return null;
    }

    private bool CheckIsWinTheWave()
    {
        if (MasterManager.spawEnemyManager)
        {
            return MasterManager.spawEnemyManager.totalEnemy <= 0;
        }

        return false;
    }

    private bool CheckIsLose()
    {
        if (!playerLife)
        {
            playerLife = FindObjectOfType<PlayerLife>();
        }

        if (playerLife)
        {
            return playerLife.IsDead;
        }

        return true;
    }

    private void PushNotification(string content, Color color)
    {
        if (MasterManager.gameHUBCanvas)
        {
            MasterManager.gameHUBCanvas.PushNotification(content, color);
        }
    }

    private void PushNotification(string content)
    {
        PushNotification(content, Color.red);
    }

    public void StartNewGame()
    {
        StartCoroutine(StartNewGameProcess());
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
