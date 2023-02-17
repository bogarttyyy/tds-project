using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Firearm : MonoBehaviour, ICollectable
{
    public static event HandleOnFirearmCollected OnFirearmCollected;
    public delegate void HandleOnFirearmCollected(ItemData itemData);

    public ItemData firearmData;

    public void Collect()
    {
        Debug.Log("Firearm Collected");
        Destroy(gameObject);
        OnFirearmCollected?.Invoke(firearmData);
    }
}
