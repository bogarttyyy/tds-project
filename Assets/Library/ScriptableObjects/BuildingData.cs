using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building Data/Generic Building")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public GameObject buildingPrefab;
    public int tier;
}
