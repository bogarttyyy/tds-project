using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Item Data/ Weapon")]
public class WeaponData : ItemData
{
    public EGunType gunType;
    public Transform projectilePrefab;
    public float projectileSpeed;
}
