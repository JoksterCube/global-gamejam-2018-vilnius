using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Wave[] waves;

    public ProtecteeManager protecteeManager;
    public EnemyManager enemyManager;

    int currentWave = 0;

    bool started = false;

    public int ProtecteesLeft
    {
        get
        {
            return ProtecteesLeftFromWave(currentWave);
        }
    }

    public int ProtecteesLeftFromWave(int index)
    {
        int amount = 0;
        for (int i = index; i < waves.Length; i++)
        {
            amount += waves[i].protecteeAmount;
        }
        return amount;
    }

    public void StartGame()
    {
        if (!started)
        {
            StartCoroutine(WaveProcess());
            started = true;
        }
    }

    IEnumerator WaveProcess()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            currentWave = i;
            protecteeManager.GiveInstructions(waves[i].protecteeAmount, waves[i].protecteeSpawnTime);
            enemyManager.GiveInstructions(GetEnemiesFromGroups(waves[i].enemyGroups));
            yield return new WaitForSeconds(waves[i].timeTillNext);
        }
    }

    Enemy[] GetEnemiesFromGroups(EnemyGroup[] enemyGroups)
    {
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < enemyGroups.Length; i++)
        {
            for (int ii = 0; ii < enemyGroups[i].size; ii++)
            {
                enemies.Add(enemyGroups[i].enemy);
            }
        }
        return enemies.ToArray();
    }
}

[System.Serializable]
public struct Wave
{
    public float timeTillNext;
    public EnemyGroup[] enemyGroups;
    public int protecteeAmount;
    public float protecteeSpawnTime;

    public Wave(float timeTillNext, EnemyGroup[] enemyGroups, int protecteeAmount, float protecteeSpawnTime)
    {
        this.timeTillNext = timeTillNext;
        this.enemyGroups = enemyGroups;
        this.protecteeAmount = protecteeAmount;
        this.protecteeSpawnTime = protecteeSpawnTime;
    }
}

[System.Serializable]
public struct EnemyGroup
{
    public Enemy enemy;
    public int size;

    public EnemyGroup(Enemy enemy, int size)
    {
        this.enemy = enemy;
        this.size = size;
    }
}