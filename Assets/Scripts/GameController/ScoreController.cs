using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public float maxTimeCombo = 3f;
    public float minTimeCombo = 0.5f;
    public int currentPoint = 0;
    public int multiply = 1;
    private GameHUBManager gameUI;
    [SerializeField]
    public float timeCountDown;

    private void Awake()
    {
        MasterManager.scoreController = this;
    }

    private void Start()
    {
        gameUI = MasterManager.gameHUBCanvas;
        timeCountDown = maxTimeCombo;
    }

    private void Update()
    {
        if(timeCountDown > 0)
        {
            timeCountDown -= Time.deltaTime;
        }
        else
        {
            timeCountDown = 0;
            multiply = 1;
        }
    }

    /// <summary>
    /// Adding score
    /// </summary>
    /// <param name="pointToAdd">amout of score to add</param>
    public void AddPoint(int pointToAdd)
    {
        currentPoint += pointToAdd * multiply;
        multiply++;
        updateTimeCountDown();
        gameUI.UpdateScoreUI(currentPoint);
        gameUI.UpdateComboUI(multiply);
    }

    private void updateTimeCountDown()
    {
        timeCountDown = Mathf.Clamp(maxTimeCombo - (maxTimeCombo * multiply / 100f),minTimeCombo,maxTimeCombo);
    }

    /// <summary>
    /// Reset score to zero
    /// </summary>
    public void ResetScore()
    {
        currentPoint = 0;
        gameUI.updateArmorUI(currentPoint);
    }
}
