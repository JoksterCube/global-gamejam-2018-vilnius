using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{
    public int startingHealth;

    public int Health { get; protected set; }

    protected bool dead;

    public event System.Action OnDeath;

    public float minYPos = -10;

    protected virtual void Start()
    {
        Health = startingHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0 && !dead)
        {
            Die();
        }
    }
    protected virtual void Update()
    {
        if (transform.position.y < minYPos)
        {
            Die();
        }
    }

    [ContextMenu("Self Destryuction")]
    public virtual void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        Destroy(gameObject);
    }
}
