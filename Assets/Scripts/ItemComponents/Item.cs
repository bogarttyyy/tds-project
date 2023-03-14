using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICollectable
{
    public static event HandleOnMaterialCollected OnMaterialCollected;
    public delegate void HandleOnMaterialCollected(ItemData itemData);

    public ItemData materialData;

    public void Collect()
    {
        Debug.Log($"Material Collected: {materialData.displayName}");
        Destroy(gameObject);
        OnMaterialCollected?.Invoke(materialData);
    }

    public ItemData GetItemData()
    {
        return materialData;
    }
}
