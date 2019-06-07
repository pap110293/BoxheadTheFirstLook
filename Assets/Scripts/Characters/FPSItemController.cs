using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FPSItemController : MonoBehaviour
{
    public ItemSlot[] itemSlots;

    private FPSItem currentItem;

    private void Start()
    {
        MasterManager.fpsItemController = this;
        ChooseItem(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (MasterManager.isPause) return;

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

    public void DisableAllItem()
    {
        foreach (var itemSlot in itemSlots)
        {
            itemSlot.Item.gameObject.SetActive(false);
        }
    }

    private void ChooseItem(int index)
    {
        if (index < 0 || index > itemSlots.Length - 1)
            return;

        var item = itemSlots[index].Item;

        if (currentItem != item && item.IsItemAvailable())
        {
            DisableAllItem();
            itemSlots[index].Item.gameObject.SetActive(true);
            currentItem = item;
        }
    }

    public bool NextItem()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            var item = itemSlots[i].Item;
            if (item == currentItem && i < itemSlots.Length - 1)
            {
                ChooseItem(i + 1);
                return true;
            }
        }
        return false;
    }

    public bool PrevItem()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            var item = itemSlots[i].Item;
            if (item == currentItem && i > 0)
            {
                ChooseItem(i - 1);
                return true;
            }
        }
        return false;
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

    public int NextFPSItemCanUnlock()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item.isLocked)
            {
                return i;
            }
        }
        return -1;
    }

    public IEnumerable<FPSItem> GetUnlockedFPSItems()
    {
        List<FPSItem> unlockedItems = new List<FPSItem>();
        foreach (var item in itemSlots)
        {
            if (!item.Item.isLocked)
            {
                unlockedItems.Add(item.Item);
            }
        }
        return unlockedItems;
    }

    public IEnumerable<FPSWeapon> GetAllWeaponItem()
    {
        var allWeapon = MasterManager.fpsItemController.GetAllItem()
                .Where(i => i is FPSWeapon)
                .Select(i => { return (FPSWeapon)i; })
                .ToArray();
        return allWeapon;
    }

    public IEnumerable<FPSItem> GetAllItem()
    {
        List<FPSItem> allItem = new List<FPSItem>();
        foreach (var item in itemSlots)
        {
            allItem.Add(item.Item);
        }
        return allItem;
    }


    public void UnlockNextItem()
    {
        int index = NextFPSItemCanUnlock();
        itemSlots[index].Item.UnlockItem();
    }
}
