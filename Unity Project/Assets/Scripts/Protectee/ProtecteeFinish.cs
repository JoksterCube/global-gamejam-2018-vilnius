using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtecteeFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Protectee")
        {
            other.GetComponent<Protectee>().Finish();
        }
    }
}
