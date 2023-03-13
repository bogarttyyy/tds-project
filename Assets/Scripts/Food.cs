using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, ICollectable
{
    public static event HandleOnCoinCollected OnFoodCollected;
    public delegate void HandleOnCoinCollected(FoodData itemData);

    public FoodData foodData;

    public void Collect()
    {
        Debug.Log("Food Collected");
        Destroy(gameObject);
        OnFoodCollected?.Invoke(foodData);
    }

    public ItemData GetItemData()
    {
        return foodData;
    }
}
