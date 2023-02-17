using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static TimeController;

public class Player : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public TopDownController topDownController;

    private InputAction inventory;

    private void OnEnable()
    {
        inventory = topDownController.playerControls.Player.Inventory;
        inventory.Enable();
        inventory.performed += OpenInventory;

        inventoryManager.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        inventory.performed -= OpenInventory;
        inventory.Disable();

    }

    private void Update()
    {
        //Debug.DrawCircle(transform.position, 1f, 32, Color.red);
    }

    private void OpenInventory(InputAction.CallbackContext context)
    {
        inventoryManager.gameObject.SetActive(!inventoryManager.gameObject.activeSelf);
    }

    public void UpdatedTimePhase(Component sender, object data)
    {
        if (data is ETimeOfDay timeOfDay)
        {
            Debug.Log($"{timeOfDay}");
        }
    }
}
