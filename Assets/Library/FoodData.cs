using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Data/Food")]
public class FoodData : ItemData
{
    public float hungerPoints;
    public EFoodType foodType;
}
