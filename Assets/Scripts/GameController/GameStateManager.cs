using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameState currentGameState;
    float playerHealth;
    const float MaxWaveLevel = 3;
    public int currWaveLevel;
    const float PrepareTime = 3;
    const float ShowDownTime = 3;
    public float waitTime;
    public int enemyCount;
    void Start()
    {
        currWaveLevel = 0;
        enemyCount = 10;
        playerHealth = 100;
    }

    public void Update()
    {
        WaitTimeUpdate();
        UpdateGameState();
        //Debug.LogError(string.Format("Current State :{0}",currentGameState));
    }
    public void ResetData()
    {
        waitTime = 0;
        currWaveLevel = 0;
    }
    
    public void UpdateGameState()
    {
        switch (currentGameState)
        {
            case GameState.NewGame:
                {
                    ResetData();                    
                    currentGameState = GameState.NewWave;
                    break;
                }
            case GameState.NewWave:
                {
                    currWaveLevel ++;
                    if (IsOver()) { currentGameState = GameState.Over; }
                    else
                    {
                        waitTime = PrepareTime;
                        currentGameState = GameState.Prepare;
                    }
                    break;
                }
            case GameState.Prepare:
                {
                    if (IsOver()) { currentGameState = GameState.Over; }
                    else
                    {                        
                        if (waitTime <= 0)
                        {                                                    
                            currentGameState = GameState.InitWave;
                        }
                    }
                    break;
                }
            case GameState.InitWave:
                {
                    if (IsOver()) { currentGameState = GameState.Over; }
                    else
                    {
                        MasterManager.spawEnemyManager.InitSpawners(MasterManager.levelConfig.GetConfigWithLevel(currWaveLevel));
                        currentGameState = GameState.Action;
                    }
                    break;
                }
            case GameState.Action:
                {
                    if (IsOver()) { currentGameState = GameState.Over; }
                    else
                    {
                        if(enemyCount<=0)
                        {
                            waitTime = ShowDownTime;
                            currentGameState = GameState.ShowDown;
                        }
                    }
                    break;
                }
            case GameState.ShowDown:
                {
                    if (IsOver()) { currentGameState = GameState.Over; }
                    else
                    {
                        if (waitTime <= 0) { currentGameState = GameState.NewWave; }
                    }
                    break;
                }
            case GameState.Over:
                {
                    break;
                }
        }
    }
    private bool IsOver()
    {
        if(playerHealth<=0)
        {
            Debug.LogError("health <=0 ");
            return true;
        }
        if (currWaveLevel >= MaxWaveLevel)
        {
            return true;
        }
        return false;
    }
    private void WaitTimeUpdate()
    {
        waitTime = waitTime - Time.fixedDeltaTime;
        //Debug.LogError(string.Format("{0}",Time.fixedDeltaTime));
    }
}
