using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform prefab;

    [SerializeField]
    private float spawnInterval = 2f;
    [SerializeField]
    private int max = 5;
    [SerializeField]
    private int min = 1;

    private List<Transform> entityList = new List<Transform>();
    private bool isSpawning;


    private void Start()
    {
        isSpawning = false;    
    }

    private void Update()
    {
        if (!isSpawning)
        {
            StartCoroutine(Spawn());
        }
    }

    public int GetSpawnCount()
    {
        return entityList.Count;
    }

    IEnumerator Spawn()
    {
        Debug.Log("Start spawn");
        while (GetSpawnCount() < max)
        {
            isSpawning = true;
            yield return new WaitForSeconds(spawnInterval);

            Debug.Log("Spawn");
        }
        Debug.Log("Stopped spawn");
        isSpawning = false;
    }

}
