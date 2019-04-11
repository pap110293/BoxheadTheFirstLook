using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField]
    private int maxHeath = 100;
    [SerializeField]
    private int maxArmor = 100;
    private int currentArmor;
    private int currentHeath;

    public int CurrentArmor { get { return currentArmor; }}
    public int CurrentHeath { get { return currentHeath; }}

    private void Start()
    {
        currentHeath = maxHeath;
    }

    public bool IsDead()
    {
        return currentHeath <= 0;
    }

    public void TakeDamage(int damage)
    {
        if (currentArmor > 0)
        {
            currentArmor -= damage;
            if(currentArmor < 0)
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

    public void Heal(int heal)
    {
        currentHeath += heal;
        currentHeath = Mathf.Clamp(currentHeath, 0, maxHeath);
    }

    public void AddArmo(int armor)
    {
        currentArmor += armor;
        currentArmor = Mathf.Clamp(currentArmor, 0, maxArmor);
    }
}
