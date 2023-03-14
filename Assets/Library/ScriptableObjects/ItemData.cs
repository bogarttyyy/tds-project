using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Data/Generic Item")]
public class ItemData : ScriptableObject
{
    public string displayName;
    public Sprite icon;
    public Transform itemPrefab;
    public bool autoPickup;
    public int maxStackCount = 1;
    public EItemType itemType;
}
