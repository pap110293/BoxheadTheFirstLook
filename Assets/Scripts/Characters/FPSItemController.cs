﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FPSItemController : MonoBehaviour
{
    public ItemSlot[] itemSlots;

    private FPSItem currentItem;

    private void Start()
    {
        ChooseItem(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChooseItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChooseItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChooseItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            ChooseItem(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            ChooseItem(4);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            ChooseItem(5);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            ChooseItem(6);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            ChooseItem(7);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            ChooseItem(8);

        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            currentItem.OnFire1();
        }
        else
        {
            currentItem.OnFire1Realse();
        }

        if (CrossPlatformInputManager.GetButton("Fire2"))
        {
            currentItem.OnFire2();
        }
        else
        {
            currentItem.OnFire2Realse();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentItem.Reload();
        }
    }

    private void DisableAllItem()
    {
        foreach (var itemSlot in itemSlots)
        {
            itemSlot.Item.gameObject.SetActive(false);
        }
    }

    private void ChooseItem(int index)
    {
        var item = itemSlots[index].Item;

        if (currentItem != item && item.IsItemAvailable())
        {
            DisableAllItem();
            itemSlots[index].Item.gameObject.SetActive(true);
            currentItem = item;
        }
    }

    [Serializable]
    public struct ItemSlot
    {
        public FPSItem Item;
    }

    public FPSItem GetCurrentItem()
    {
        return currentItem;
    }
}