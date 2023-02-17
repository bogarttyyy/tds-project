using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string displayName;
    public Sprite icon;
    public bool autoPickup;
    public int maxStackCount = 1;
    public EItemType timeType;
}
