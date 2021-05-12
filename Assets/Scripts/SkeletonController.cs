using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SkeletonController : MonoBehaviour
{
    public float ForwardSpeed;
    public float TurnSpeed;
    public Animator AnimController;

    CharacterController Controller;
    

    void Start()
    {
        Controller = GetComponent<CharacterController>();    
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") > 0f)
        {
            Vector3 move = transform.forward * ForwardSpeed * Time.deltaTime;

            Controller.Move(move);

            AnimController.SetBool("Walk", true);
        }
        else
        {
            AnimController.SetBool("Walk", false);
        }

        Controller.transform.Rotate(new Vector3(0f, Input.GetAxis("Horizontal") * TurnSpeed * Time.deltaTime, 0f));

        //Attack
    }
}
