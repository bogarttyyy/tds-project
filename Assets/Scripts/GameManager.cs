using System;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Player player;

    [HorizontalLine(color: EColor.Gray)]


    [BoxGroup("Food")]
    public UIProgressBar hungerBar;
    [BoxGroup("Food")]
    [SerializeField] private int foodCapacity;
    [BoxGroup("Food")]
    [SerializeField] private int currentFood;
    [BoxGroup("Food")]
    [Label("Hunger Rate")]
    [SerializeField] private int hungerRatePerSecond;
    [BoxGroup("Person")]
    [SerializeField] private int personCapacity;
    [BoxGroup("Person")]
    [SerializeField] private int currentPerson;
    [BoxGroup("Wood")]
    [SerializeField] private int woodCapacity;
    [BoxGroup("Wood")]
    [SerializeField] private int currentWood;
    [SerializeField]
    private List<Building> buildingList = new List<Building>();

    private void Start()
    {
        StartGlobalSystemCoroutines();
    }

    private void StartGlobalSystemCoroutines()
    {
        // Start all Coroutines here
        StartCoroutine(GoHungry());
    }

    private void Update()
    {
        // List all UI updates here
        UpdateHungerUI();
    }

    IEnumerator GoHungry()
    {
        while (currentFood > 0)
        {
            yield return new WaitForSeconds(1);
            currentFood -= hungerRatePerSecond;
        }
    }

    private void UpdateHungerUI()
    {
        // Hunger currently set to 200 before reflecting on the progressbar
        if (currentFood <= 200)
        {
            hungerBar.progressValue.fillAmount =  currentFood / 200f;
        }
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void HandleOnBuildingPlaced(Component component, object data)
    {
        Building building = data as Building;
        buildingList.Add(building);
        AddResourceCapacity(building);
    }

    public void HandleOnBuildingRemoved(Component component, object data)
    {
        Building building = data as Building;
        buildingList.Remove(building);
        SubtractResourceCapacity(building);
    }

    private void AddResourceCapacity(Building building)
    {
        if (building.buildingData is StorageBuildingData storageData)
        {
            switch (storageData.buildingType)
            {
                case EBuildingType.Person:
                    personCapacity += storageData.capacity;
                    break;
                case EBuildingType.Food:
                    foodCapacity += storageData.capacity;
                    break;
                case EBuildingType.Wood:
                    woodCapacity += storageData.capacity;
                    break;
                case EBuildingType.Pet:
                default:
                    break;
            }
        }
    }

    private void SubtractResourceCapacity(Building building)
    {
        Debug.Log($"In Building list: {buildingList.Contains(building)}");

        if (buildingList.Contains(building))
        {
            if (building.buildingData is StorageBuildingData storageData)
            {
                switch (storageData.buildingType)
                {
                    case EBuildingType.Person:
                        personCapacity -= storageData.capacity;
                        break;
                    case EBuildingType.Food:
                        foodCapacity -= storageData.capacity;
                        break;
                    case EBuildingType.Wood:
                        woodCapacity -= storageData.capacity;
                        break;
                    case EBuildingType.Pet:
                    default:
                        break;
                }
            }
        }

        Destroy(building);
    }
}
