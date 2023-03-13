using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private InputAction action;

    private IInteractable interactable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            action.performed += InteractPerformed;
            action.Enable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ActionUnsub();
    }

    private void OnDisable()
    {
        ActionUnsub();
    }

    private void InteractPerformed(InputAction.CallbackContext obj)
    {
        interactable.Interact();
    }

    private void ActionUnsub()
    {
        if (action != null)
        { 
            action.Disable();
            action.performed -= InteractPerformed;
        }
    }
}
