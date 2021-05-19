using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blendtree2Dcontroller : MonoBehaviour
{
    public Animator Anim;

    void Start()
    {
        
    }

    void Update()
    {
        Anim.SetFloat("VerticalMove", Input.GetAxis("Vertical"));
        Anim.SetFloat("HorizontalMove", Input.GetAxis("Horizontal"));
    }
}
