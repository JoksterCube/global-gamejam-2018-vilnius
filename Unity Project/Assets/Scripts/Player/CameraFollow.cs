using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public float smoothTime = .5f;

    private Vector3 velocity;

    private void LateUpdate()
    {
        if (target == null) { return; }

        Vector3 newPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
}
