using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Header("Attributes")]
    
    [SerializeField] private int currencyWorth = 0;
    public bool isDestroyed = false;
    
    [SerializeField] private int maxHealth = 10;
    private int _health;
    public int Health {
        get
        {
            return _health;
        }
        private set
        {
            _health += value;

            if(_health <= 0)
            {
                _health = 0;
                isDestroyed = true;

                Destroy(gameObject);
                EnemySpawner.onDestroyEnemy.Invoke();
            }
        }
    }

    private void Start()
    {
        _health = maxHealth;
    }

    public void GotHit(int damage)
    {
        Health = -damage;
    }
}
