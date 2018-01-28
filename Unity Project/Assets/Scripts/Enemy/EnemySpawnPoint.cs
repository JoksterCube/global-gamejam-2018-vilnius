using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public float radius = 1;
    EnemyManager enemyManager;

    private void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
    }

    public Enemy Spawn(Enemy enemyPrefab)
    {
        if (enemyManager == null || enemyPrefab == null) { return null; }
        // TODO: add everything to enemies
        Enemy enemy = Instantiate(enemyPrefab, GetRandomPositionInRadius(), Quaternion.identity, enemyManager.enemyParent) as Enemy;
        enemy.enemyManager = enemyManager;

        enemy.GetComponent<LifeBarController>().lifeBarParent = enemyManager.lifeBarParrent;

        return enemy;
    }

    public Vector3 GetRandomPositionInRadius()
    {
        float newX = Random.Range(-radius, radius);
        float newZ = Random.Range(-radius, radius);

        return (new Vector3(newX, transform.position.y, newZ)).normalized;
    }
}
