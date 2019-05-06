using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PickUpItemGranade : PickUpItemBase
{

    public override void PickUpItem()
    {
        base.PickUpItem();
        var unlockedGenade = MasterManager.fpsItemController.GetUnlockedFPSItems()
            .FirstOrDefault(i => i is FPSItemToThrow && i.itemName == "Granade");

        if(unlockedGenade)
        {
            unlockedGenade.AddMore(1);
        }
    }
}
