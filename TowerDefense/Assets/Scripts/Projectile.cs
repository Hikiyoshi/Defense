using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private int Damage = 2;
    [SerializeField] private float speed = 5f;

    private Transform target;
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if(target == null || Vector2.Distance(transform.position, target.position) <= 0.5f) 
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(!target)
            return;
        
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();

        if(damageable != null)
        {
            damageable.GotHit(Damage);
            Destroy(gameObject);
        }
    }

}
