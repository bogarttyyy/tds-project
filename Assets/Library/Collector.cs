using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Collector : MonoBehaviour
{
    private InputAction interact;
    private ICollectable item;

    private void OnEnable()
    {
        if (gameObject.TryGetComponent<TopDownController>(out var controller))
        {
            interact = controller.playerControls.Player.Interact;
            interact.performed += Pickup;
        }
    }

    private void OnDisable()
    {
        interact.performed -= Pickup;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        item = collision.GetComponent<ICollectable>();

        if (item != null)
        {
            var itemData = item.GetItemData();

            if (!itemData.autoPickup)
            {
                interact.Enable();
            }
            else
            {
                item.Collect();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interact != null)
        {
            interact.Disable();
        }
    }

    public void Pickup(InputAction.CallbackContext context)
    {
        item.Collect();
    }
}
