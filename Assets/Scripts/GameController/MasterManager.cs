﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MasterManager : MonoBehaviour
{
    public static bool isPause = false;
    public static GameHUBManager gameHUBCanvas;
    public static FPSItemController fpsItemController;
    public static ItemSpawnerController itemSpawnerController;
    public static MenuInGameController menuInGameController;
    public static GameController gameController;
    public static LevelConfigManager levelConfigManager;
    public static SpawEnemyManager spawEnemyManager;
    public static ScoreController scoreController;
    public static EnemyLevelConfigManager enemyLevelConfigManager;
    public static void PauseGame()
    {
        isPause = true;
        Time.timeScale = 0f;
    }

    public static void ResumeGame()
    {
        isPause = false;
        Time.timeScale = 1f;
    }

    public static void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void UnLockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}