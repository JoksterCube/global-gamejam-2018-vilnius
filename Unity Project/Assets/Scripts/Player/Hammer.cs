using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public Transform head;
    public float startAngle;
    public float endAngle;
    public float range = 10f;
    public float attackRange = 5f;

    public int damage;
    public float attackTime;
    public float cooldownTime;

    public float turnSpeed = 10f;
    public float lookingTime = 0.5f;

    public float life;

    public string enemyTag = "Enemy";
    Transform target;
    bool canAttack = true;
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, lookingTime);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float minDist = float.MaxValue;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < minDist && distanceToEnemy < range)
            {
                nearestEnemy = enemy;
                minDist = distanceToEnemy;
            }
        }
        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
    }

    private void Update()
    {
        if (target != null && canAttack)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            float targetDistance = Vector3.Distance(transform.position, target.position);
            Vector3 enemyDir = target.position - transform.position;
            enemyDir.y = 0;
            enemyDir = enemyDir.normalized;
            if (Utility.Approximate(enemyDir, transform.forward, .1f))
            {
                if (targetDistance < attackRange && canAttack)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        float percent = 0;
        float speed = 1 / attackTime;
        while (percent < 1)
        {
            percent += Time.deltaTime * speed;

            float rotation = Mathf.Lerp(startAngle, endAngle, percent);

            head.localRotation = Quaternion.Euler(rotation, 0, 90);
            yield return null;
        }
        if (target != null)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        yield return Reload();
    }

    IEnumerator Reload()
    {
        float percent = 0;
        float speed = 1 / cooldownTime;
        while (percent < 1)
        {
            percent += Time.deltaTime * speed;

            float rotation = Mathf.Lerp(endAngle, startAngle, percent);

            head.localRotation = Quaternion.Euler(rotation, 0, 90);
            yield return null;
        }
        canAttack = true;
    }

    public void Dying()
    {
        Invoke("SelfDestroy", life);
    }
    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
