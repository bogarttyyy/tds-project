using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChange;

    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    private void OnEnable()
    {
        Coin.OnCoinCollected += Add;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= Add;
    }

    public void Add(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item) && item.CanStack())
        {
            // if found, add to stack
            item.AddToStack();
            Debug.Log($"{item.itemData.displayName} total stack: {item.stackSize}");
            OnInventoryChange?.Invoke(inventory);
        }
        else
        {
            // if not, create inventoryItem then add
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            Debug.Log($"New inventory entry: {newItem.itemData.displayName}");
            OnInventoryChange?.Invoke(inventory);
        }
    }

    public void Remove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if(item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
            OnInventoryChange?.Invoke(inventory);
        }
    }

    internal List<InventoryItem> GetInventoryList()
    {
        return inventory;
    }
}
