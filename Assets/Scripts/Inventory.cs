using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Weapon weapon;
    private Armor armor;

    public int getWeaponDamage()
    {
        if(weapon != null)
        {
            return weapon.damage;
        }
        else
        {
            return 20;
        }    
    }

    public float getWeaponMod()
    {
        if (weapon != null)
        {
            return weapon.armorMod;
        }
        else
        {
            return 1;
        }
    }

    public float getArmorDmgMod()
    {
        if(armor != null)
        {
            return armor.damageMod;
        }
        else
        {
            return 1.0f;
        }
    }

    public float getArmorMvspeedMod()
    {

        if (armor != null)
        {
            return armor.movespeedMod;
        }
        else
        {
            return 1.0f;
        }
    }

    public Armor getArmor()
    {
        return armor;
    }

    public void setWeapon(Weapon wpn)
    {
        this.weapon = wpn;
    }

    public void setArmor(Armor armr)
    {
        this.armor = armr;
    }
}
