using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameEvent onEnemyDeath;

    [SerializeField]
    private float health;

    public Player player;

    private void Start()
    {
        
    }

    internal void Damage()
    {
        // Implement health

        // Death
        onEnemyDeath.Raise(this, gameObject);
        Destroy(gameObject);
    }



}
