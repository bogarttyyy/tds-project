using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTester : MonoBehaviour
{
    private Grid<int> grid;
    private void Start()
    {
        grid = new Grid<int>(2f, 4, 4);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            grid.SetValue(mousePos, 3);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            int val = grid.GetValueAt(mousePos);
            Debug.Log($"Value got from grid: {val}");
        }
    }
}
