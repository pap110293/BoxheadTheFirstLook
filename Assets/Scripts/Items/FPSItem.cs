using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSItem : MonoBehaviour
{
    public int amount = 0;
    public int maxAmount = 1;
    [HideInInspector]
    public bool fire1, fire2;

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
        return amount > 0;
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

}
