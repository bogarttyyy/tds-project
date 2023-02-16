using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static TimeController;

public class Player : MonoBehaviour
{
    public InventoryManager inventoryManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryManager.gameObject.SetActive(!inventoryManager.gameObject.activeSelf);
        }
    }

    public void UpdatedTimePhase(Component sender, object data)
    {
        if (data is ETimeOfDay timeOfDay)
        {
            Debug.Log($"{timeOfDay}");
        }
    }
}
