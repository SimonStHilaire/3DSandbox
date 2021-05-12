using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Renderer>().enabled = false;
        Invoke("Reactivate", 2f);
    }

    void Reactivate()
    {
        GetComponent<Renderer>().enabled = true;
    }
}
