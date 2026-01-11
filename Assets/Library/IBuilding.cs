using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding : IInteractable
{
    public BuildingData GetBuildingData();
    public void Build();
}
