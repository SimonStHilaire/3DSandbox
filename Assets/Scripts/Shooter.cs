using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform Target;
    public GameObject ProjectilePrefab;

    public enum ShootType
    {
        ForceUp,
        ForceDown,
        Random
    }

    public ShootType shootType;

    public float ShootSpeed;

    public void Shoot(Vector3? pos = null)
    {
        Vector3 shootPosition = pos.HasValue ? pos.Value : Target.position;

        Vector3 vec = shootPosition - transform.position;
        Ray ray = new Ray(transform.position, vec);

        bool up = Physics.Raycast(ray, vec.magnitude - 0.01f);
        
        float? angle = ComputeAngle(shootPosition, up);

        if (angle != null)
        {
            transform.LookAt(shootPosition);

            transform.localEulerAngles = new Vector3(360f - (float)angle, transform.localEulerAngles.y, transform.localEulerAngles.z);

            GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);

            projectile.GetComponent<Rigidbody>().velocity = transform.forward * ShootSpeed;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }
    }

    float? ComputeAngle(Vector3 pos, bool up = true)
    {
        Vector3 dir = pos - transform.position;

        float y = dir.y;

        dir.y = 0f;

        float distance = dir.magnitude;

        float speedSq = ShootSpeed * ShootSpeed;

        float gravity = Mathf.Abs(Physics.gravity.y);

        float validate = (speedSq * speedSq) - gravity * (gravity * distance * distance + 2 * y * speedSq);

        if (validate < 0)
            return null;

        if(up)
            return Mathf.Atan2(speedSq + Mathf.Sqrt(validate), gravity * distance) * Mathf.Rad2Deg;
        else
            return Mathf.Atan2(speedSq - Mathf.Sqrt(validate), gravity * distance) * Mathf.Rad2Deg;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "shoot", true);
    }
#endif //UNITY_EDITOR
}
