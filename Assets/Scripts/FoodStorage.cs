using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStorage : MonoBehaviour, IBuilding, IInteractable
{
    public StorageBuildingData buildingData;

    public BuildingData GetBuildingData()
    {
        return buildingData;
    }
    public void Interact(object obj = null)
    {
        Debug.Log("House Interact");
    }
}
