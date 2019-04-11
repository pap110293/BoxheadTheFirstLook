using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSItem : MonoBehaviour
{
    public int ItemIndex;
    public string Info = "";
    public ItemCollector CollectorSlot;
    [HideInInspector]
    public bool OnFire1, OnFire2;
    public virtual void Trigger()
    {
        OnFire1 = true;
    }
    public virtual void Trigger2()
    {
        OnFire2 = true;
    }
    public virtual void OnTriggerRelease()
    {
        OnFire1 = false;
    }
    public virtual void OnTrigger2Release()
    {
        OnFire2 = false;
    }
    public virtual bool Reload()
    {
        return true;
    }
    public virtual void OnAction()
    {

    }
}
