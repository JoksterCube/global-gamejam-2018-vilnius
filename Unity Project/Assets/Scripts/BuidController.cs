using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuidController : MonoBehaviour
{

    public Hammer hammerPrefab;

    public Vector3 offset;
    public float lifeTime = 5;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Build();
        }
    }

    void Build()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 0;
        newPosition += transform.forward + offset;
        Hammer hammer = Instantiate(hammerPrefab, newPosition, Quaternion.identity) as Hammer;
        hammer.life = lifeTime;
        hammer.Dying();
    }
}
