using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float force;
    public float radius;
    public float upModifier;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody[] objs = FindObjectsOfType<Rigidbody>();

        foreach(Rigidbody rb in objs)
        {
            rb.AddExplosionForce(force, transform.position, radius, upModifier);
        }

        Destroy(gameObject);
    }
}
