using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class VisibleObject : MonoBehaviour
{
    MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void Show()
    {
        if(!mesh.enabled)
            mesh.enabled = true;
    }

    public void Hide()
    {
        if(mesh.enabled)
            mesh.enabled = false;
    }
}
