using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public CharacterController Controller;
    public Animator AnimatorController;
    public Collider KickCollider;

    public float Acceleration;
    public float Deceleration;
    public float MaxSpeed;
    public float TurnSpeed;

    float Speed = 0f;

    public void KickColliderOn()
    {
        if (KickCollider != null)
            KickCollider.enabled = true;
    }

    public void KickColliderOff()
    {
        if (KickCollider != null)
            KickCollider.enabled = false;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            AnimatorController.SetTrigger("Kick");
        }

        if (Controller != null)
        {
            if (Input.GetAxis("Vertical") > 0f)
            {
                Speed += Time.deltaTime * Acceleration;
            }
            else
            {
                Speed -= Time.deltaTime * Deceleration;
            }

            Speed = Mathf.Clamp(Speed, 0f, MaxSpeed);

            Vector3 move = transform.forward * Speed * Time.deltaTime;

            Controller.Move(move);
            Controller.transform.Rotate(new Vector3(0f, Input.GetAxis("Horizontal") * TurnSpeed * Time.deltaTime, 0f));
        }
    }
}
