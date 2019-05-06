using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int PrepareTime = 3;

    private PlayerLife playerLife;
    private bool isGamePlaying = false;
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
        }
    }

    IEnumerator loadLevel(LevelData level)
    {
        if(level == null) yield return null;
        if (MasterManager.spawEnemyManager)
        {
            MasterManager.spawEnemyManager.InitSpawners(level);
        }
        for (int i = 0; i < PrepareTime; i++)
        {
            PushNotification("Start in : " + (PrepareTime - i));
            yield return new WaitForSeconds(1f);
        }

        PushNotification("Start wave " + (MasterManager.levelConfigManager.currentLeve));

        if (MasterManager.spawEnemyManager)
        {
            MasterManager.spawEnemyManager.StartAll();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator GameProcess()
    {
        yield return StartCoroutine(StartNewWave());
        yield return StartCoroutine(Playing());

        if(CheckIsLose())
        {
            StartCoroutine(LosingProcess());
        }
        else if(CheckIsWinTheWave())
        {
            StartCoroutine(WiningProcess());
        }
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
        PushNotification("you win!!!");
        yield return null;
    }

    private bool CheckIsWinTheWave()
    {
        if(MasterManager.spawEnemyManager)
        {
            return MasterManager.spawEnemyManager.totalEnemy <= 0;
        }

        return false;
    }

    private bool CheckIsLose()
    {
        if(!playerLife)
        {
            playerLife = GameObject.FindObjectOfType<PlayerLife>();
        }

        if(playerLife)
        {
            return playerLife.IsDead();
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
