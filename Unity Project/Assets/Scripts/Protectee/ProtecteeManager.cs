using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtecteeManager : MonoBehaviour
{
    public float spawnRateSeconds;
    public int spawnAmount;
    float nextSpawnTime = 0;

    public ProtecteeSpawn spawn;
    public ProtecteeFinish finish;
    public Waypoints waypoints;
    public Transform protecteeParent;

    public List<Protectee> protectees;

    public bool simulation = false;

    public Transform lifeBarParrent;

    private void Awake()
    {
        protectees = new List<Protectee>();
        if (!simulation)
        {
            spawnAmount = 0;
            spawnRateSeconds = 0;
            nextSpawnTime = 0;
        }
    }


    private void Update()
    {
        if (spawnAmount > 0)
        {
            if (nextSpawnTime <= Time.time)
            {
                spawn.Spawn();
                nextSpawnTime = Time.time + spawnRateSeconds;
                spawnAmount--;
            }
        }
    }

    public Transform GetNextWaypoint(ref int currentWaypointIndex)
    {
        if (currentWaypointIndex >= waypoints.points.Length - 1)
        {
            return finish.transform;
        }

        currentWaypointIndex++;

        return waypoints.points[currentWaypointIndex];
    }

    public void GiveInstructions(int protecteeAmount, float protecteeSpawnTime)
    {
        spawnAmount = protecteeAmount;
        spawnRateSeconds = protecteeSpawnTime;
        nextSpawnTime = 0;
    }

    public void ProtecteeFinished(Protectee protectee)
    {
        GetComponentInParent<GameManager>().OneFinished();
        protectees.Remove(protectee);
    }
    public void ProtecteeDestroyed(Protectee protectee)
    {
        //Debug.Log("Protectee Destroyed");
        protectees.Remove(protectee);
    }
}
