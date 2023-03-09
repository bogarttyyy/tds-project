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

    private Rigidbody2D projectileRb;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    //private void Start()
    //{
    //    projectileRb = GetComponent<Rigidbody2D>();
    //}

    public void Setup(Vector3 shootDir, float projectileSpeed)
    {
        this.shootDir = shootDir;
        //NOTE: Change this Vector to correctly rotate depending on where it goes
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        speed = projectileSpeed;
        //transform.LookAt(shootDir);
        //projectileRb = GetComponent<Rigidbody2D>();
        //projectileRb.AddForce(Vector2.up);
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
