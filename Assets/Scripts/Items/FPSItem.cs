using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSItem : MonoBehaviour
{
    public int maxAmount = 1;
    [HideInInspector]
    public bool fire1, fire2;
    public bool isLocked = true;
    public string itemName;

    [SerializeField]
    protected int amount = 0;
    public virtual void OnFire1()
    {
        fire1 = true;
    }

    public virtual void OnFire2()
    {
        fire2 = true;
    }

    public virtual void OnFire1Realse()
    {
        fire1 = false;
    }

    public virtual void OnFire2Realse()
    {
        fire2 = false;
    }

    public virtual bool IsItemAvailable()
    {
        return amount > 0 && !isLocked;
    }

    public virtual bool Reload()
    {
        return true;
    }

    public virtual void OnAction()
    {

    }

    public virtual void ReloadComplete()
    {

    }

    public virtual void UnlockItem()
    {
        isLocked = false;
    }

    public void AddMore(int amount)
    {
        this.amount += amount;
        if (this.amount > maxAmount)
            this.amount = maxAmount;
        else
            MasterManager.gameHUBCanvas.PushNotification(itemName + " add " + amount, Color.black);
    }
}
