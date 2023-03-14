using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour, ICollectable
{
    public static event HandleOnWoodCollected OnWoodCollected;
    public delegate void HandleOnWoodCollected(ItemData itemData);

    public ItemData woodData;

    public void Collect()
    {
        Debug.Log("Wood Collected");
        Destroy(gameObject);
        OnWoodCollected?.Invoke(woodData);
    }

    public ItemData GetItemData()
    {
        return woodData;
    }
}
