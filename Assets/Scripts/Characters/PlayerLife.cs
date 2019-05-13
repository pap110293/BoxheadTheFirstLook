using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(Rigidbody))]
public class PlayerLife : LifeBase
{
    public bool isImportal = false;

    public bool isDead = false;

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

            GetComponent<FirstPersonController>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            GetComponent<SphereCollider>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = false;
            MasterManager.fpsItemController.DisableAllItem();
        }
    }

    public void Restart()
    {
        isDead = false;

        GetComponent<FirstPersonController>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<CharacterController>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
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

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            DieNow();
    }

    private void DieNow()
    {
        TakeDamage(this.maxHP);
    }
}
