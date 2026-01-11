using UnityEngine;

public class Grid<T>
{
    private int width;
    private int height;
    private float cellSize;


    private T[,] gridArray;

    public Grid(float cellSize)
    {
        this.cellSize = cellSize;
        
        // TEMPORARY DEFAULT
        // Change logic later
        gridArray = new T[10,10];

        GenerateGrid();
    }

    public Grid(float cellSize, int x, int y)
    {
        width = x;
        height = y;
        this.cellSize = cellSize;

        gridArray = new T[width,height];
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x,y) * cellSize;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
    }

    public Vector3 GetCellWordPosition(int x, int y)
    {
        Vector3 coordinateWorldPos = GetWorldPosition(x, y);
        float halfCell = cellSize / 2;
        return new Vector3(coordinateWorldPos.x + halfCell, coordinateWorldPos.y + halfCell);
    }

    public Vector3 GetCellWorldPosition(Vector3 worldPosition)
    {
        GetXY(worldPosition, out int x, out int y);
        return GetCellWordPosition(x, y);
    }

    public void SetValue(int x, int y, T value)
    {
        // NOTE: This limits the grid to the set width and height
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x,y] = value;
            Debug.Log($"Setting value: {value} at {x},{y}");
        }
    }

    public void SetValue(Vector3 worldPosition, T value)
    {
        GetXY(worldPosition, out int x, out int y);
        SetValue(x, y, value);
    }

    public T GetValueAt(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }

        return default;
    }

    public T GetValueAt(Vector3 worldPosition)
    {
        Debug.Log("attempting value get");
        GetXY(worldPosition, out int x, out int y);
        return GetValueAt(x, y);
    }
}
