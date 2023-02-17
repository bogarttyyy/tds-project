using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public static event HandleOnCoinCollected OnCoinCollected;
    public delegate void HandleOnCoinCollected(ItemData itemData);

    public ItemData coinData;

    public void Collect()
    {
        Debug.Log("Coin Collected");
        Destroy(gameObject);
        OnCoinCollected?.Invoke(coinData);
    }
}
