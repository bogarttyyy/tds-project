using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;
    
    private Vector3 shootDir;

    private int playerLayer;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.position += shootDir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ignore player layer
        //Debug.Log($"Collision Detected!: {LayerMask.LayerToName(collision.gameObject.layer)}");
        if (collision.gameObject.layer != playerLayer)
        {
            Destroy(gameObject);
        }

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Damage();
        }

    }
}
