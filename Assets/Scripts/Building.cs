using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IBuilding, IInteractable
{
    public BuildingData buildingData;

    public BuildingData GetBuildingData()
    {
        return buildingData;
    }

    public void Interact(object obj = null)
    {
        Debug.Log($"{buildingData.buildingName} Interact");
    }
}
