using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public Inventory inventory;
    public List<InventorySlot> slots = new(12);

    private void OnEnable()
    {
        Inventory.OnInventoryChange += DrawInventory;
        DrawInventory(inventory.GetInventoryList());
    }

    private void OnDisable()
    {
        Inventory.OnInventoryChange -= DrawInventory;
    }

    private void ResetInventory()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }

        slots = new List<InventorySlot>(12);
    }

    private void DrawInventory(List<InventoryItem> newInventory)
    {
        ResetInventory();

        for (int i = 0; i < slots.Capacity; i++)
        {
            // Create the slots in the inventory
            CreateInventorySlot();
        }

        for (int i = 0; i < newInventory.Count; i++)
        {
            slots[i].DrawSlot(newInventory[i]);
        }
    }

    private void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();
        slots.Add(newSlotComponent);
    }
}
