using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtecteeSpawn : MonoBehaviour
{
    public Protectee protecteePrefab;
    ProtecteeManager protecteeManager;

    private void Awake()
    {
        protecteeManager = GetComponentInParent<ProtecteeManager>();
    }

    public void Spawn()
    {
        if (protecteeManager == null || protecteePrefab == null) { return; }
        //Debug.Log("Spawn");
        Protectee protectee = Instantiate(protecteePrefab, transform.position, Quaternion.identity, protecteeManager.protecteeParent) as Protectee;
        protectee.protecteeManager = protecteeManager;
        protecteeManager.protectees.Add(protectee);

        protectee.OnFinish -= protecteeManager.ProtecteeFinished;
        protectee.OnFinish += protecteeManager.ProtecteeFinished;

        protectee.OnDeath -= protecteeManager.ProtecteeDestroyed;
        protectee.OnDeath += protecteeManager.ProtecteeDestroyed;

        protectee.GetComponent<LifeBarController>().lifeBarParent = protecteeManager.lifeBarParrent;
    }
}
