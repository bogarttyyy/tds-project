using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour, IBuilding, IInteractable
{
    public StorageBuildingData buildingData;

    public void Build()
    {
        throw new System.NotImplementedException();
    }

    public BuildingData GetBuildingData()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(object obj = null)
    {
        Debug.Log("House Interact");
    }
}
