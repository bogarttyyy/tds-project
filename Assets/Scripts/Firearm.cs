using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Firearm : MonoBehaviour, ICollectable
{
    public static event HandleOnFirearmCollected OnFirearmCollected;
    public delegate void HandleOnFirearmCollected(ItemData itemData);

    public ItemData firearmData;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = firearmData.icon;
    }

    public void Collect()
    {
        Debug.Log("Firearm Collected");
        Destroy(gameObject);
        OnFirearmCollected?.Invoke(firearmData);
    }
}
