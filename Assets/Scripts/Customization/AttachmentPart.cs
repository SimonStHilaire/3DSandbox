using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentPart : MonoBehaviour
{
    public AttachmentType type;

    [SerializeField]
    private string title;
    [SerializeField]
    private string description;

    public string Title
    {
        get{

            if (!string.IsNullOrEmpty(title))
                return title;

            return gameObject.name;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
