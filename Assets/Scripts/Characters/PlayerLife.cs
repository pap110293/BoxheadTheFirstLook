using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : LifeBase
{
    public bool isImportal = false;

    private bool isDead = false;

    public override void Init()
    {
        base.Init();
        updateUI();
    }

    public override void TakeDamage(int damage)
    {
        if (!isImportal)
        {
            base.TakeDamage(damage);
            updateUI();
        }
    }

    public override void OnDead()
    {
        if (!isDead)
        {
            base.OnDead();
            isDead = true;
            Debug.Log("The player is dead!!!");
        }
    }

    private void updateUI()
    {
        MasterManager.gameHUBCanvas.updateArmorUI(CurrentArmor);
        MasterManager.gameHUBCanvas.UpdateHPUI(CurrentHP);
    }

    public override void Heal(int heal)
    {
        base.Heal(heal);
        updateUI();
        // show text
        MasterManager.gameHUBCanvas.PushNotification("Heal " + heal, Color.green);
    }
}
