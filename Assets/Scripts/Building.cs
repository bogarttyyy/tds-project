using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Building : MonoBehaviour, IBuilding
{
    public BuildingData buildingData;

    private List<Collider2D> colliders;

    public RequiredMaterials[] currentMaterials;

    public bool isPlaced;
    public bool inProgress;
    public bool isBuilt;
    private void OnEnable()
    {
        isPlaced = false;
        colliders = GetComponentsInChildren<Collider2D>().ToList();

        if (TryGetComponent(out Collider2D mainCollider))
        {
            colliders.Add(mainCollider);
        }

        EnableColliders(false);
    }

    public void BuildingPlaced()
    {
        isPlaced = true;
        EnableColliders(true);
    }

    public void EnableColliders(bool enabled)
    {
        foreach (var collider in colliders)
        {
            collider.enabled = enabled;
        }
    }

    public BuildingData GetBuildingData()
    {
        return buildingData;
    }

    public abstract void Interact(object obj = null);

    public void Build()
    {
        RequiredMaterials[] material = buildingData.materialsRequired;
    }
}
