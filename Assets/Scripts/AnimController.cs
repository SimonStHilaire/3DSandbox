
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimController : MonoBehaviour
{
    Animator Controller;
    public float Speed;
    public float StepSize;

    void Start()
    {
        Controller = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * Speed * StepSize;
        Controller.SetFloat("Speed", Speed);
    }
}