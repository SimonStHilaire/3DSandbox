using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public float ForwardSpeed;
    public float TurnSpeed;
    public Animator AnimController;
    public Collider SwordCollider;
    public CharacterController Controller;
    public bool CanAttack;

    private bool CanMove = true;

    void Start()
    {
 
    }

    void Update()
    {
        if (CanMove && Input.GetAxis("Vertical") > 0f)
        {
            Vector3 move = transform.forward * ForwardSpeed * Time.deltaTime;

            Controller.Move(move);

            AnimController.SetBool("Walk", true);
        }
        else
        {
            AnimController.SetBool("Walk", false);
        }

        if(CanMove)
            Controller.transform.Rotate(new Vector3(0f, Input.GetAxis("Horizontal") * TurnSpeed * Time.deltaTime, 0f));

        //Attack
        if (CanAttack && CanMove && Input.GetButton("Fire1"))
        {
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
