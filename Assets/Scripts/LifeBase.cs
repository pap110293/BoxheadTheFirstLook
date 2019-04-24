using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBase : MonoBehaviour
{
    public int maxHP = 100;
    public int maxArmor = 100;
    private int currentArmor;
    private int currentHP;

    public int CurrentArmor { get { return currentArmor; } }
    public int CurrentHP { get { return currentHP; } }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (IsDead())
            OnDead();
        else
            OnUpdate();
    }

    public virtual void Init()
    {
        currentHP = maxHP;
    }

    public virtual bool IsDead()
    {
        return currentHP <= 0;
    }

    public virtual void TakeDamage(int damage)
    {
        if (currentArmor > 0)
        {
            currentArmor -= damage;
            if (currentArmor < 0)
            {
                currentHP -= currentArmor;
                currentArmor = 0;
            }
        }
        else
        {
            currentHP -= damage;
        }

        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    public virtual void Heal(int heal)
    {
        currentHP += heal;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    public virtual void AddArmo(int armor)
    {
        currentArmor += armor;
        currentArmor = Mathf.Clamp(currentArmor, 0, maxArmor);
    }

    public virtual void OnDead()
    {

    }

    public virtual void OnUpdate()
    {

    }
}
