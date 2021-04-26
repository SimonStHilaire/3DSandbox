using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialControl : MonoBehaviour
{
    public float Factor;
    Material TargetMaterial;
    public float Speed;

    public enum GlowType
    {
        Show,
        Hide
    }

    public GlowType glowType;

    void Start()
    {
        TargetMaterial = GetComponent<Renderer>().sharedMaterial;
    }

    void Update()
    {
        switch(glowType)
        {
            case GlowType.Hide:
                Factor -= Time.deltaTime * Speed;
                Factor = Mathf.Clamp01(Factor);
                break;
            case GlowType.Show:
                Factor += Time.deltaTime * Speed;
                Factor = Mathf.Clamp01(Factor);
                break;
        }

        TargetMaterial.SetFloat("_factor", Factor);
    }
}
