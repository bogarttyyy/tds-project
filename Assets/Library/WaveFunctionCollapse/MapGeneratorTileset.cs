using Assets.Library;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class MapGeneratorTileset : MonoBehaviour
{
    private Grid<GameObject> grid;

    public GameObject forestTile;
    public GameObject grassTile;
    public GameObject sandTile;
    public GameObject waterTile;

    private Dictionary<int, GameObject> tileSet;

    private int width, height;
    private Queue<Vector2Int> coordinatesForChecking;

    [SerializeField]
    private int queueCount;

    private void Awake()
    {
        // Initialize Tileset
        tileSet = new Dictionary<int, GameObject>
        {
            { 0, forestTile },
            { 1, grassTile },
            { 2, sandTile },
            { 3, waterTile }
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<GameObject>(1f, width, height);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.GetXY(CommonHelper.GetMouseWorldPos2D(), out int x, out int y);
            Debug.Log($"{x}, {y}");

            PlaceTile();
        }
    }

    private void PlaceTile()
    {
        grid.GetXY(CommonHelper.GetMouseWorldPos2D(), out int x, out int y);
        coordinatesForChecking = new Queue<Vector2Int>();
        coordinatesForChecking.Enqueue(new Vector2Int(x, y));

        while (coordinatesForChecking.Count > 0)
        {
            CheckAdjacentTiles();
        }
    }

    IEnumerator CheckAdjacentTiles()
    {
        // Dequeue coordinate from 
        var coord = coordinatesForChecking.Dequeue();
        // Instantiate and set value of tile in the grid
        ;
        GameObject tile = Instantiate(tileSet[SelectTileToPlace()], grid.GetCellWordPosition(coord.x, coord.y), Quaternion.identity);
        grid.SetValue(coord.x, coord.y, tile);

        // Then check up, right, down, left neighouring tiles
        // Let's assume that we don't have negative coordinates
        if (coord.y + 1 <= height)
        {
            AddToCoordinateToQueue(coord.x, coord.y + 1);
        }
        if (coord.x + 1 <= width)
        {
            AddToCoordinateToQueue(coord.x + 1, coord.y);
        }
        if (coord.y - 1 >= 0)
        {
            AddToCoordinateToQueue(coord.x, coord.y - 1);
        }
        if (coord.x - 1 >= 0)
        {
            AddToCoordinateToQueue(coord.x - 1, coord.y);
        }

        queueCount = coordinatesForChecking.Count;

        yield return new WaitForSeconds(1f);
    }

    private int SelectTileToPlace()
    {
        return Random.Range(0, 3);
    }

    private void AddToCoordinateToQueue(int x, int y)
    {
        // Add to queue if null
        if (grid.GetValueAt(x, y) == null)
        {
            coordinatesForChecking.Enqueue(new Vector2Int(x, y));
        }
    }
}
