using Assets.Library;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    public Player player;
    
    [Header("Selected")]
    [SerializeField]
    private GameObject selectedBuilding;

    [Header("Bindings")]
    public InputAction activateBuildMode;
    public InputAction build;
    public InputAction remove;
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
    public GameEvent onBuildingDestroyed;

    private PlayerControls playerControls;
    private Grid<Building> buildingGrid;

    private bool isBuildModeActivated;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        activateBuildMode = playerControls.Player.BuildMode;
        activateBuildMode.Enable();
        activateBuildMode.performed += ActivateBuildMode;
        build.performed += Build_performed;
        remove.performed += Remove_performed;
        selectHouse.performed += SelectHouse_performed;
        selectFood.performed += SelectFood_performed;
        selectWood.performed += SelectWood_performed;

    }

    private void OnDisable()
    {
        activateBuildMode.Disable();
        activateBuildMode.performed -= ActivateBuildMode;
        build.performed -= Build_performed;
        remove.performed -= Remove_performed;
        selectHouse.performed -= SelectHouse_performed;
        selectFood.performed -= SelectFood_performed;
        selectWood.performed -= SelectWood_performed;

    }

    private void Start()
    {
        buildingGrid = new Grid<Building>(cellSize: 1f);
    }

    private void Update()
    {
        if (isBuildModeActivated)
        {
            build.Enable();
            remove.Enable();
            selectHouse.Enable();
            selectFood.Enable();
            selectWood.Enable();
        }
        else
        {
            build.Disable();
            remove.Disable();
            selectHouse.Disable();
            selectFood.Disable();
            selectWood.Disable();
        }

        if (selectedBuilding != null)
        {
            selectedBuilding.transform.position = CommonHelper.GetMouseWorldPos2D();
        }
    }

    private void ActivateBuildMode(InputAction.CallbackContext obj)
    {
        Debug.Log("Activate Build Mode");
        isBuildModeActivated = !isBuildModeActivated;
        player.BuildModeActive(isBuildModeActivated);
    }

    private void SelectWood_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Wood Selected!");
        HoverBuildLocation(woodStoragePrefab);
    }

    private void SelectFood_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Food Selected!");
        HoverBuildLocation(foodStoragePrefab);
    }

    private void SelectHouse_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("House Selected!");
        HoverBuildLocation(housePrefab);
    }

    private void HoverBuildLocation(GameObject prefab)
    {
        if (selectedBuilding != null)
        {
            Destroy(selectedBuilding);
        }

        Vector3 mousePos = CommonHelper.GetMouseWorldPos2D();
        selectedBuilding = Instantiate(prefab, mousePos, Quaternion.identity);
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

    private void Remove_performed(InputAction.CallbackContext obj)
    {
        RemoveBuilding();
    }

    private void PlaceBuilding()
    {
        Vector3 mousePos = CommonHelper.GetMouseWorldPos2D();
        selectedBuilding.transform.position = mousePos;

        //GameObject building = Instantiate(selectedBuilding, mousePos, Quaternion.identity);
        Building data = selectedBuilding.GetComponent<Building>();
        onBuildingPlaced.Raise(this, data);
        Debug.Log($"{data.buildingData.buildingName} placed!");

        // Clean SelectedBuilding variable
        selectedBuilding = null;
    }

    private void RemoveBuilding()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        
        // TODO

        //if (hit.rigidbody)
        //{
        //    if (hit.transform.gameObject.TryGetComponent<Building>(out var building))
        //    {
        //        onBuildingDestroyed.Raise(this, building);
        //    }
        //}
        
    }
}
