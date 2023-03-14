using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

public class Building : MonoBehaviour, IBuilding, IInteractable
{
    public BuildingData buildingData;

    private List<Collider2D> colliders;

    public bool isPlaced;

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

    public void Interact(object obj = null)
    {
        Debug.Log($"{buildingData.buildingName} Interact");
    }
}
