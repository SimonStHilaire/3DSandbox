using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OrientOnVelocity : MonoBehaviour
{
    public float MagnitudeThreshold;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent < Rigidbody>();
    }

    void Update()
    {
        if (rb.velocity.magnitude > MagnitudeThreshold)
        {
            transform.forward = rb.velocity;
        }
    }
}
