using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemHealth : PickUpItemBase
{
    [Range(0f,100f)]
    public int healPercent = 30;
    public override void PickUpItem()
    {
        base.PickUpItem();
        if (MasterManager.fpsItemController == null) return;

        var life = MasterManager.fpsItemController.GetComponent<PlayerLife>();
        if (life == null) return;

        life.Heal(life.maxHP * healPercent / 100);
    }
}
