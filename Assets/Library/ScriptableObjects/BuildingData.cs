using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Building Data/Generic Building")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public GameObject buildingPrefab;
    public int tier;
    public RequiredMaterials[] materialsRequired;
}

[System.Serializable]
public struct RequiredMaterials
{
    public EMaterialType materialType;
    public int materialNumber;
}
