using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonMovement : MonoBehaviour
{
    public float time = 1f;

    public Vector3 initialPoint;
    public Vector3 endPoint;

    public Transform piston;

    void Start()
    {
        if (piston != null)
        {
            StartCoroutine(Piston());
        }
    }

    IEnumerator Piston()
    {
        float percent = 0;
        float speed = 1 / time;

        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            piston.localPosition = Vector3.Lerp(initialPoint, endPoint, interpolation);
            yield return null;
        }
        yield return Piston();
    }
}
