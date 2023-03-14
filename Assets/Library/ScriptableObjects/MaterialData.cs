using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Data/Material")]
public class MaterialData : ItemData
{
    public EMaterialType materialType;
    public int tier = 1;
    public int value;
}
