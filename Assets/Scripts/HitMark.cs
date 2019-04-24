//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------

using UnityEngine;

public struct DamagePackage
{
    public Vector3 Position;
    public Vector3 Direction;
    public Vector3 Normal;
    public int Damage;
    public int ID;
    public byte Team;
    public byte DamageType;
}

public class HitMark : MonoBehaviour
{
    public GameObject HitFX;
    public float DamageMult = 1;
    // 8 must be a Hitbox Layer;
    public int HitboxPhysicLayer = 8;

    private LifeBase life;
    private void Awake()
    {
        this.gameObject.layer = HitboxPhysicLayer;
    }

    void Start()
    {
        if (this.transform.root)
        {
            life = this.transform.root.GetComponent<LifeBase>();
        }
        else
        {
            life = this.transform.GetComponent<LifeBase>();
        }
    }

    public void OnHit(DamagePackage pack)
    {
        if (life)
        {
            // apply damage to damage manager
            int alldamage = (int)(pack.Damage * DamageMult);

            life.TakeDamage(alldamage);
        }

        // add particle effect at hit position
        ParticleFX(pack.Position, pack.Normal);
    }
    public void OnHitTest(DamagePackage pack)
    {
        if (life)
        {
            // show hit effect in crosshair
            if (UnitZ.gameManager != null && UnitZ.gameManager.PlayerNetID == pack.ID)
            {
                if (UnitZ.playerManager.PlayingCharacter != null && UnitZ.playerManager.PlayingCharacter.inventory != null)
                {
                    if (UnitZ.playerManager.PlayingCharacter.inventory.FPSEquipment != null)
                    {
                        if (UnitZ.playerManager.PlayingCharacter.inventory.FPSEquipment.GetComponent<Crosshair>())
                        {
                            UnitZ.playerManager.PlayingCharacter.inventory.FPSEquipment.GetComponent<Crosshair>().Hit();
                        }
                    }
                }
            }
        }

        ParticleFX(pack.Position, pack.Normal);

    }
    public void ParticleFX(Vector3 position, Vector3 normal)
    {
        if (HitFX)
        {
            GameObject fx = (GameObject)GameObject.Instantiate(HitFX, position, Quaternion.identity);
            fx.transform.forward = normal;
            GameObject.Destroy(fx, 3);
        }
    }
}
