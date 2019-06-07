using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUBManager : MonoBehaviour
{

    private GameObject weaponCamera;
    private Camera mainCamera;
    private float defaultFOV;
    [SerializeField]
    private Text hp, armor;
    [SerializeField]
    private GameObject notification;
    [SerializeField]
    private GameObject notificationGroup;
    private int defaultComboSize;
    private ScoreController scoreController;
    private float tempFontsize;

    public GameObject crossHair;
    public GameObject crossHairZoom;

    public Text ammoText;
    public Text scoreText;
    public Text waveText;

    [Header("Combo Text")]
    public Text comboText;
    public Color startComboColor;
    public Color endComboColor;

    private void Awake()
    {
        weaponCamera = GameObject.FindGameObjectWithTag("WeaponCamera");
        mainCamera = Camera.main;
        defaultFOV = mainCamera.fieldOfView;
        MasterManager.gameHUBCanvas = this;
    }

    private void Start()
    {
        MasterManager.ResumeGame();
        defaultComboSize = comboText.fontSize;
        scoreController = MasterManager.scoreController;
    }

    private void Update()
    {
        if (comboText)
        {
            if (comboText.fontSize > defaultComboSize)
            {
                tempFontsize -= 0.1f;
                comboText.fontSize = (int)tempFontsize;
            }

            if (scoreController)
            {
                var comboColor = comboText.color;
                comboColor.a = scoreController.timeCountDown / 2 > 1f ? 1f : scoreController.timeCountDown / 2;
                comboText.color = comboColor;
            }
        }
    }

    public void Scoped(float FOVZoom)
    {
        if (crossHair && crossHairZoom)
        {
            crossHair.SetActive(false);
            crossHairZoom.SetActive(true);
            weaponCamera.SetActive(false);
            mainCamera.fieldOfView = FOVZoom;
        }
    }

    public void UnScoped()
    {
        if (crossHair && crossHairZoom)
        {
            crossHair.SetActive(true);
            crossHairZoom.SetActive(false);
            weaponCamera.SetActive(true);
            mainCamera.fieldOfView = defaultFOV;
        }
    }

    public void UpdateHPUI(int hp)
    {
        if (this.hp)
        {
            if (hp < 0) hp = 0;
            this.hp.text = hp + "";
        }
    }

    public void updateArmorUI(int armor)
    {
        if (this.armor)
        {
            if (armor < 0) armor = 0;
            this.armor.text = armor + "";
        }
    }

    public void PushNotification(string content)
    {
        this.PushNotification(content, Color.blue);
    }

    public void PushNotification(string content, Color color)
    {
        var notificationObj = Instantiate(notification, notificationGroup.transform);
        var notificationText = notificationObj.GetComponent<Text>();
        if (notificationText)
        {
            notificationText.color = color;
            notificationText.text = content;
        }
    }

    public void UpdateAmmoUI(int current, int have)
    {
        if (!ammoText.gameObject.activeSelf)
            ammoText.gameObject.SetActive(true);
        ammoText.text = current + " / " + have;
    }

    public void DisableAmmoUI()
    {
        ammoText.gameObject.SetActive(false);
    }

    public void UpdateScoreUI(int point)
    {
        if (!scoreText) return;

        scoreText.text = point + "";
    }

    public void UpdateComboUI(int combo)
    {
        if (!comboText) return;
        comboText.fontSize = 60;
        comboText.text = "x" + combo;
        tempFontsize = comboText.fontSize;
        UpdateComboTextColor(combo);
    }

    private void UpdateComboTextColor(int combo)
    {
        comboText.color = Color.Lerp(startComboColor, endComboColor, Mathf.Clamp(combo / 100f, 0, 100f));
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = "Wave " + wave;
    }
}
