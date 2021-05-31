using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public WheelCollider FrontLeft, FrontRight, RearLeft, RearRight;
    public Transform FrontLeftTransform, FrontRightTransform, RearLeftTransform, RearRightTransform;
    public LayerMask GroundLayerMask;
    public Transform TurrentTransform;

    public float Acceleration;
    public float Deceleration;

    public float SteerAngle;

    void Start()
    {
        
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        float forwardInput = Input.GetAxis("Vertical");

        bool movement = false;

        if (!Mathf.Approximately(forwardInput, 0f))
        {
            FrontRight.motorTorque = Acceleration * forwardInput;
            RearRight.motorTorque = Acceleration * forwardInput;
            RearLeft.motorTorque = Acceleration * forwardInput;
            FrontLeft.motorTorque = Acceleration * forwardInput;

            movement = true;
        }

        if (!Mathf.Approximately(horizontalInput, 0f))
        {
            FrontRight.motorTorque = -Acceleration * horizontalInput;
            RearRight.motorTorque = -Acceleration * horizontalInput;
            RearLeft.motorTorque = Acceleration * horizontalInput;
            FrontLeft.motorTorque = Acceleration * horizontalInput;

            movement = true;
        }

       if (movement)
        {
            FrontLeft.brakeTorque = 0f;
            FrontRight.brakeTorque = 0f;
            RearLeft.brakeTorque = 0f;
            RearRight.brakeTorque = 0f;
        }
        else
        {
            FrontLeft.brakeTorque = Deceleration;
            FrontRight.brakeTorque = Deceleration;
            RearLeft.brakeTorque = Deceleration;
            RearRight.brakeTorque = Deceleration;
        }

       //Visuel
        SetTransform(FrontLeft, FrontLeftTransform);
        SetTransform(FrontRight, FrontRightTransform);
        SetTransform(RearLeft, RearLeftTransform);
        SetTransform(RearRight, RearRightTransform);

        UpdateTurret();
    }

    void Update4WheelDriveAndSteering()
    {
        float forwardInput = Input.GetAxis("Vertical");

        if (!Mathf.Approximately(forwardInput, 0f))
        {
            FrontLeft.brakeTorque = 0f;
            FrontRight.brakeTorque = 0f;
            RearLeft.brakeTorque = 0f;
            RearRight.brakeTorque = 0f;

            FrontLeft.motorTorque = Acceleration * forwardInput;
            FrontRight.motorTorque = Acceleration * forwardInput;
            RearLeft.motorTorque = Acceleration * forwardInput;
            RearRight.motorTorque = Acceleration * forwardInput;
        }
        else
        {
            FrontLeft.brakeTorque = Deceleration;
            FrontRight.brakeTorque = Deceleration;
            RearLeft.brakeTorque = Deceleration;
            RearRight.brakeTorque = Deceleration;
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        FrontLeft.steerAngle = SteerAngle * horizontalInput;
        FrontRight.steerAngle = SteerAngle * horizontalInput;
        RearRight.steerAngle = SteerAngle * horizontalInput * -1f;
        RearLeft.steerAngle = SteerAngle * horizontalInput * -1f;

        //Visuel
        SetTransform(FrontLeft, FrontLeftTransform);
        SetTransform(FrontRight, FrontRightTransform);
        SetTransform(RearLeft, RearLeftTransform);
        SetTransform(RearRight, RearRightTransform);
    }

    void SetTransform(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;

        collider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }

    void UpdateTurret()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit = new RaycastHit();

        if(Physics.Raycast(ray, out raycastHit, float.MaxValue, GroundLayerMask))
        {
            Vector3 lookAt = raycastHit.point;
            lookAt.y = TurrentTransform.position.y;
            TurrentTransform.LookAt(lookAt);
        }
    }
}
