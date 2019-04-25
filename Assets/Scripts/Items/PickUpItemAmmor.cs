using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpItemAmmor : PickUpItemBase
{
    public override void ExecuteItem()
    {
        base.ExecuteItem();
        if (MasterManager.fpsItemController)
        {
            var unlockedGuns = MasterManager.fpsItemController.GetUnlockedFPSItems()
            .Where(i => i is FPSWeapon)
            .Select(i => { return (FPSWeapon)i; })
            .Where(i => i.weaponType == FPSWeapon.WeaponType.Gun)
            .ToArray();

            if (unlockedGuns.Length > 0)
            {
                var weapon = unlockedGuns[Random.Range(0, unlockedGuns.Count())];
                weapon.AddAmmor(weapon.clipSize);
            }

        }
    }
}
