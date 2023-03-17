using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CampFire : Building, IInteractable
{
    [SerializeField]
    private bool isLit;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    public void Interact(object obj = null)
    {
        isLit = !isLit;

        if (!isLit)
        {
            spriteRenderer.color = Color.yellow;
        }
        else
        {
            spriteRenderer.color = new Color(0.66f, 0.34f, 0, 1);
        }
    }
}
