using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpItemAmmo : PickUpItemBase
{
    public override void PickUpItem()
    {
        base.PickUpItem();
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
                weapon.AddAmmo(weapon.clipSize);
            }

        }
    }
}
