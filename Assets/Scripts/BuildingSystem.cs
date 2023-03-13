using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    public Player player;
    
    [Header("Selected")]
    [SerializeField]
    private GameObject selectedBuilding;

    [Header("Bindings")]
    public InputAction build;
    [HorizontalLine]
    public InputAction selectHouse;
    public InputAction selectFood;
    public InputAction selectWood;
    
    [HorizontalLine]
    [Header("Prefabs")]
    [SerializeField]
    private GameObject housePrefab;
    [SerializeField]
    private GameObject foodStoragePrefab;
    [SerializeField]
    private GameObject woodStoragePrefab;

    public GameEvent onBuildingPlaced;


    private void OnEnable()
    {
        build.performed += Build_performed;
        selectHouse.performed += SelectHouse_performed;
        selectFood.performed += SelectFood_performed;
        selectWood.performed += SelectWood_performed;

    }

    private void OnDisable()
    {
        build.performed -= Build_performed;
        selectHouse.performed -= SelectHouse_performed;
        selectFood.performed -= SelectFood_performed;
        selectWood.performed -= SelectWood_performed;

    }

    private void Update()
    {
        // TEMPORARY POC: If player has equip, disable build
        if (!player.HasEquip())
        {
            build.Enable();
            selectHouse.Enable();
            selectFood.Enable();
            selectWood.Enable();
        }
        else
        {
            build.Disable();
            selectHouse.Disable();
            selectFood.Disable();
            selectWood.Disable();
        }
    }

    private void SelectWood_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Wood Selected!");
        selectedBuilding = woodStoragePrefab;
    }

    private void SelectFood_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Food Selected!");
        selectedBuilding = foodStoragePrefab;
    }

    private void SelectHouse_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("House Selected!");
        selectedBuilding = housePrefab;
    }

    private void Build_performed(InputAction.CallbackContext obj)
    {
        if (selectedBuilding != null)
        {
            PlaceBuilding();
        }
        else
        {
            Debug.Log("You must select a building");
        }
    }

    private void PlaceBuilding()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        GameObject building = Instantiate(selectedBuilding, mousePos, Quaternion.identity);
        Building data = building.GetComponent<Building>();
        onBuildingPlaced.Raise(this, data);
        Debug.Log($"{data.buildingData.buildingName} placed!");
    }
}
