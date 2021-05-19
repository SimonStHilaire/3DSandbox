using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public float Acceleration;
    public float Deceleration;
    public float MaxSpeed;
    public float TurnSpeed;
    public Animator AnimController;
    public Collider SwordCollider;
    public CharacterController Controller;
    public bool CanAttack;

    [Range(0f, 1f)]
    public float Health;

    private bool CanMove = true;
    float Speed = 0f;

    void Start()
    {
 
    }

    void Update()
    {
        if (CanMove && Input.GetAxis("Vertical") > 0f)
        {
            Speed += Time.deltaTime * Acceleration;
        }
        else
        {
            Speed -= Time.deltaTime * Deceleration;
        }

        float maxSpeed = MaxSpeed * Health;

        Speed = Mathf.Clamp(Speed, 0f, maxSpeed);

        Vector3 move = transform.forward * Speed * Time.deltaTime;

        Controller.Move(move);

        AnimController.SetFloat("Movement", Speed / MaxSpeed);
        AnimController.SetFloat("Health", Health);

        if (CanMove)
            Controller.transform.Rotate(new Vector3(0f, Input.GetAxis("Horizontal") * TurnSpeed * Time.deltaTime, 0f));

        //Attack
        if (CanAttack && CanMove && Input.GetButton("Fire1"))
        {
            Speed = 0f;
            AnimController.SetTrigger("Attack");
        }
    }

    public void ActivateMove()
    {
        CanMove = true;
    }

    public void DeactivateMove()
    {
        CanMove = false;
    }

    public void ActivateSword()
    {
        SwordCollider.enabled = true;
    }

    public void DeactivateSword()
    {
        SwordCollider.enabled = false;
    }
}
