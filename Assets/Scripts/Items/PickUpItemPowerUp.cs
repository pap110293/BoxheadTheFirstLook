using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpItemPowerUp : PickUpItemBase
{
    public int damageToAdd = 10;
    public override void PickUpItem()
    {
        base.PickUpItem();
        if (MasterManager.fpsItemController)
        {
            var unlockedItems = MasterManager.fpsItemController.GetUnlockedFPSItems()
                .Where(i => i is FPSWeapon)
                .Select(i => { return (FPSWeapon)i; })
                .ToArray();

            if (unlockedItems.Length > 0)
            {
                var weapon = unlockedItems[Random.Range(0, unlockedItems.Count())];
                weapon.AddDamage(damageToAdd);
            }
        }
    }
}
