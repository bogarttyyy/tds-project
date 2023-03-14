using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChange;
    public static event Action<List<InventoryItem>> OnFirearmAddedToInventory;

    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    private void OnEnable()
    {
        Coin.OnCoinCollected += Add;
        Firearm.OnFirearmCollected += AddFirearm;
        Material.OnMaterialCollected += AddMaterial;
        Food.OnFoodCollected += AddFood;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= Add;
        Firearm.OnFirearmCollected -= AddFirearm;
        Material.OnMaterialCollected -= AddMaterial;
        Food.OnFoodCollected -= AddFood;
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

    public void AddFirearm(ItemData itemData)
    {
        Debug.Log("Attempting to add Firearm");
        if (itemData.itemType is EItemType.Weapon)
        {
            Add(itemData);
            OnFirearmAddedToInventory?.Invoke(GetFirearms());
        }
    }

    private void AddMaterial(MaterialData itemData)
    {
        Add(itemData);
    }

    private void AddFood(FoodData itemData)
    {
        Add(itemData);
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

    public bool HasWeapon()
    {
        return inventory.Any(item => item.itemData.itemType == EItemType.Weapon);
    }

    public InventoryItem GetWeapon()
    {
        var firearm = inventory.FirstOrDefault(f => f.itemData.itemType == EItemType.Weapon);

        if (firearm != null)
        {
            return firearm;
        }

        return null;
    }
    

    private InventoryItem GetFirearm(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            return item;
        }
        
        return null;
    }

    private List<InventoryItem> GetFirearms()
    {
        List<InventoryItem> weapons = new List<InventoryItem>();
        foreach (InventoryItem item in inventory)
        {
            if (item.itemData.itemType is EItemType.Weapon)
            {
                weapons.Add(item);
            }
        }

        return weapons;
    }

    internal List<InventoryItem> GetInventoryList()
    {
        return inventory;
    }
}
