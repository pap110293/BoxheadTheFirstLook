using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int PrepareTime = 5;

    private PlayerLife playerLife;
    private bool isGamePlaying = false;
    private bool isDone = false;

    void Start()
    {
        StartCoroutine(GameProcess());
    }

    private IEnumerator StartNewWave()
    {
        MasterManager.spawEnemyManager.totalEnemy = 0;
        var levelData = MasterManager.levelConfigManager.GetNextLevelData();
        if (levelData != null)
        {
            yield return StartCoroutine(loadLevel(levelData));
            isDone = false;
        }
        else
        {
            isDone = true;
        }
    }

    IEnumerator loadLevel(LevelData level)
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

        PushNotification("START WAVE " + (MasterManager.levelConfigManager.currentLevel));

        if (MasterManager.spawEnemyManager)
        {
            MasterManager.spawEnemyManager.StartAll();
        }
    }

    IEnumerator GameProcess()
    {
        yield return StartCoroutine(StartNewWave());

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

    private IEnumerator WinAllWaveProcess()
    {

        PushNotification("YOU WIN ALL THE WAVE");
        PushNotification("YOU WIN ALL THE WAVE");
        PushNotification("YOU WIN ALL THE WAVE");
        PushNotification("YOU WIN ALL THE WAVE");
        PushNotification("YOU WIN ALL THE WAVE");
        PushNotification("YOU WIN ALL THE WAVE");

        for (int i = 0; i < PrepareTime; i++)
        {
            PushNotification("BACK TO MAIN MENU IN " + (PrepareTime - i));
            yield return new WaitForSeconds(1f);
        }
        MasterManager.menuInGameController.BackToMenu();
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
        PushNotification("you Lose!!!");
        //MasterManager.menuInGameController.ShowLosePanel();
        yield return null;
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
            playerLife = GameObject.FindObjectOfType<PlayerLife>();
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
}
