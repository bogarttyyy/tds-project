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

    [SerializeField]
    private bool hasEquipped;
    [SerializeField]
    private bool isInBuildMode;
    [SerializeField]
    private bool canAttack;

    private void OnEnable()
    {
        inventoryOpen = topDownController.playerControls.Player.Inventory;
        inventoryOpen.Enable();
        inventoryOpen.performed += OpenInventory;

        inventoryUI.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (inventoryOpen != null)
        {
            inventoryOpen.performed -= OpenInventory;
            inventoryOpen.Disable();
        }
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

        EnableAttackCheck();

        //weaponAnchor.localPosition = new Vector3(0.5f, 0);

        SpriteRenderer sprite = weapon.gameObject.GetComponent<SpriteRenderer>();
        CircleCollider2D collider = weapon.GetComponent<CircleCollider2D>();
        collider.enabled = false;
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

    public bool EnableAttackCheck()
    {
        if (hasEquipped && !isInBuildMode)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        return canAttack;
    }

    public bool HasEquip()
    {
        return hasEquipped;
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

    public void BuildModeActive(bool isActive)
    {
        if (isActive)
        {
            inventoryUI.gameObject.SetActive(false);
        }

        isInBuildMode = isActive;
    }

    public bool CanAttack()
    {
        return canAttack;
    }
}
