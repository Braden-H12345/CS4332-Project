using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon" )]
public class Weapon : ScriptableObject
{
    public new string name;

    public Sprite image;

    public int damage;

    public float armorMod;
}
