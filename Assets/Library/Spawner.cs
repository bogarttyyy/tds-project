using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    private List<GameObject> entityList = new List<GameObject>();

    private bool isSpawning;

    private void Start()
    {
        isSpawning = false;    
    }

    private void Update()
    {
        if (!isSpawning && GetSpawnCount() < max)
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
            Vector3 randomizedX = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y);
            entityList.Add(Instantiate(prefab, randomizedX, Quaternion.identity).gameObject);
            Debug.Log("Spawn");
        }
        Debug.Log("Stopped spawn");
        isSpawning = false;
    }

    [Button("Remove One")]
    private void Despawn()
    {
        if (entityList.Any())
        {
            GameObject entity = entityList.First();
            entityList.Remove(entity);
            Destroy(entity);
        }
    }


    public void Despawn(Component sender, object gameObject)
    {
        Debug.Log("Attempting Despawn");
        entityList.Remove((GameObject)gameObject);
    }
}
