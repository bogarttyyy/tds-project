using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Player player;

    [HorizontalLine(color: EColor.Gray)]
    public UIProgressBar hungerBar;

    [SerializeField]
    private int hungerCapacity;
    [SerializeField]
    private int currentHunger;
    [SerializeField]
    private int hungerRatePerSecond;

    [HorizontalLine(color: EColor.Orange)]
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
        while (currentHunger > 0)
        {
            yield return new WaitForSeconds(1);
            currentHunger -= hungerRatePerSecond;
        }
    }

    private void UpdateHungerUI()
    {
        // Hunger currently set to 200 before reflecting on the progressbar
        if (currentHunger <= 200)
        {
            hungerBar.progressValue.fillAmount =  currentHunger / 200f;
        }
    }


    public Player GetPlayer()
    {
        return player;
    }
}
