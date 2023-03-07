using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public Inventory inventory;
    public List<InventorySlot> slots = new(2);

    private void OnEnable()
    {
        Inventory.OnFirearmAddedToInventory += DrawHotbar;
    }

    private void OnDisable()
    {
        Inventory.OnFirearmAddedToInventory -= DrawHotbar;
    }

    private void ResetHotbar()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }

        slots = new List<InventorySlot>(2);
    }

    private void DrawHotbar(List<InventoryItem> newInventory)
    {
        ResetHotbar();

        for (int i = 0; i < slots.Capacity; i++)
        {
            CreateHotbarSlot();
        }

        int j = 0;
        while (j < slots.Capacity && j < newInventory.Count)
        {
            slots[j].DrawSlot(newInventory[j]);
            j++;
        };
    }

    private void CreateHotbarSlot(InventoryItem item = null)
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();

        Image bgColor = newSlotComponent.GetComponent<Image>();
        bgColor.color = new Color(189, 255, 235);
        slots.Add(newSlotComponent);
    }
}
