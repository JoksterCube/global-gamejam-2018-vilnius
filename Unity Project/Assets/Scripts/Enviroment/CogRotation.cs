using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogRotation : MonoBehaviour
{
    public float rotationSpeed = 15f;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
