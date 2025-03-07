using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Transform target;

    private int pathIndex = 0;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
    }

    private void Update()
    {
        if(Vector2.Distance(target.position, transform.position) < 0.1f)
        {
            pathIndex++;
        }

        if(pathIndex == LevelManager.main.path.Length)
        {
            Destroy(gameObject);
            EnemySpawner.onDestroyEnemy.Invoke();
            return;
        }
        
        target = LevelManager.main.path[pathIndex];
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed * Time.deltaTime;
    }



}
