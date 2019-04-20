using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBase : MonoBehaviour
{
    public int maxHeath = 100;
    public int maxArmor = 100;
    private int currentArmor;
    private int currentHeath;

    public int CurrentArmor { get { return currentArmor; } }
    public int CurrentHeath { get { return currentHeath; } }

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
        currentHeath = maxHeath;
    }

    public virtual bool IsDead()
    {
        return currentHeath <= 0;
    }

    public virtual void TakeDamage(int damage)
    {
        if (currentArmor > 0)
        {
            currentArmor -= damage;
            if (currentArmor < 0)
            {
                currentHeath -= currentArmor;
                currentArmor = 0;
            }
        }
        else
        {
            currentHeath -= damage;
        }

        currentHeath = Mathf.Clamp(currentHeath, 0, maxHeath);
    }

    public virtual void Heal(int heal)
    {
        currentHeath += heal;
        currentHeath = Mathf.Clamp(currentHeath, 0, maxHeath);
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
