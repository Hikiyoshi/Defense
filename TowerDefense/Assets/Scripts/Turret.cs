using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private Transform target;

    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")]

    [SerializeField] private float attackRange = 3f;

    [SerializeField] private float firePerSeconds = 1f;

    private float timeUntilFire = 0f;

    private void Update()
    {
        if(target == null)
        {
            FindTarget();
            return;
        }

        if(!CheckTargetIsInRange())
        {
            target = null;
        }

        if(target != null && timeUntilFire >= 1f/firePerSeconds)
        {
            Shoot();
            timeUntilFire = 0f;
        }

        timeUntilFire += Time.deltaTime;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hit2Ds = Physics2D.CircleCastAll(transform.position, attackRange, transform.position, 0f, enemyMask);

        if(hit2Ds.Length > 0)
        {
            target = hit2Ds[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(transform.position, target.position) <= attackRange;
    }

    private void Shoot()
    {
        GameObject projectileObj = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        
        if(projectile != null)
        {
            projectile.SetTarget(this.target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Handles.color = Color.gray;
        Handles.DrawWireDisc(transform.position, transform.forward ,attackRange);
    }

}
