using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static TimeController;

public class Player : MonoBehaviour
{
    public InventoryManager inventoryUI;
    public Inventory inventory;
    public TopDownController topDownController;

    public Transform weaponAnchor;

    private InputAction inventoryOpen;

    public GameEvent onEquipFirearm;

    private Transform weapon;

    private bool hasEquipped;

    private void OnEnable()
    {
        inventoryOpen = topDownController.playerControls.Player.Inventory;
        inventoryOpen.Enable();
        inventoryOpen.performed += OpenInventory;

        inventoryUI.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        inventoryOpen.performed -= OpenInventory;
        inventoryOpen.Disable();
    }

    private void Update()
    {
        if (inventory.HasWeapon())
        {
            EquipWeapon(inventory.GetWeapon());
        }
    }

    private void EquipWeapon(InventoryItem item)
    {
        if (!weaponAnchor.gameObject.activeSelf)
        {
            //weaponAnchor.gameObject.SetActive(true);
            //item.itemData.itemPrefab.gameObject
        }

        if (!hasEquipped)
        {
            hasEquipped = true;
            weapon = Instantiate(item.itemData.itemPrefab.gameObject, weaponAnchor.position, Quaternion.identity, transform).transform;
            onEquipFirearm.Raise(this, weapon);
        }

        //weaponAnchor.localPosition = new Vector3(0.5f, 0);

        SpriteRenderer sprite = weapon.gameObject.GetComponent<SpriteRenderer>();
        //sprite.sprite = item.itemData.icon;


        Vector3 dir = weapon.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        weapon.localPosition = Vector3.ClampMagnitude(dir.normalized * -1, 0.6f);

        if (dir.x > 0)
        {
            sprite.flipY = false;
        }
        else
        {
            sprite.flipY = true;
        }

        sprite.flipX = true;
    }

    private void OpenInventory(InputAction.CallbackContext context)
    {
        inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
    }

    public void UpdatedTimePhase(Component sender, object data)
    {
        if (data is ETimeOfDay timeOfDay)
        {
            Debug.Log($"{timeOfDay}");
        }
    }
}
