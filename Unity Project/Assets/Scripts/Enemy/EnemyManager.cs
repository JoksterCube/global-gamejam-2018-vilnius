using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemySpawnPoint[] spawnPoints;
    public ProtecteeManager protecteeManager;
    public Transform enemyParent;

    public Transform lifeBarParrent;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<EnemySpawnPoint>();
    }

    public Enemy SpawnEnemy(Enemy enemy)
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex].Spawn(enemy);
    }

    public Protectee GetNearestProtectee(Vector3 position)
    {
        int minIndex = -1;
        float minDist = float.MaxValue;
        Protectee[] protectees = protecteeManager.protectees.ToArray();
        for (int i = 0; i < protecteeManager.protectees.Count; i++)
        {
            float dist = Vector3.Distance(protectees[i].transform.position, position);
            if (dist < minDist)
            {
                minIndex = i;
                minDist = dist;
            }
        }
        return (minIndex != -1) ? protectees[minIndex] : null;
    }

    public void GiveInstructions(Enemy[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            SpawnEnemy(enemies[i]);
        }
    }
}
