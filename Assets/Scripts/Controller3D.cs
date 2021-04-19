using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller3D : MonoBehaviour
{
    public MainCharacter Character;
    public List<VisibleObject> Objects;

    public ViewType CurrentViewType;

    public float Angle;

    public float QuadWidth;
    public float QuadHeight;

    public enum ViewType
    {
        All,
        Front,
        AngleCone,
        None
    }

    void Start()
    {
        
    }

    void Update()
    {
        switch(CurrentViewType)
        {
            case ViewType.All:
                ShowAll();
                break;

            case ViewType.Front:
                ShowFrontOnly();
                break;

            case ViewType.AngleCone:
                ShowInAngleOnly();
                break;

            case ViewType.None:
                HideAll();
                break;

            default:
                ShowAll();
                break;
        }
    }

    public void GenerateMesh()
    {
        GameObject newObj = new GameObject("New quad mesh");

        MeshFilter meshFilter = newObj.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = newObj.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

        Vector3[] vertices = {
            new Vector3(0, 0, 0),
            new Vector3(QuadWidth, 0, 0),
            new Vector3(0, QuadHeight, 0),
            new Vector3(QuadWidth, QuadHeight, 0),
        };

        meshFilter.mesh.vertices = vertices;

        int[] triangles = {
            0, 2, 1,
            2, 3, 1
            };

        meshFilter.mesh.triangles = triangles;

        Vector3[] normals = {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
        };

        meshFilter.mesh.normals = normals;

        //*uv
    }

    void ShowFrontOnly()
    {
        foreach (VisibleObject vo in Objects)
        {
            Vector3 vec = vo.transform.position - Character.transform.position;
            float dot = Vector3.Dot(vec, -Character.transform.forward);
            
            if (dot >= 0)
                vo.Show();
            else
                vo.Hide();
        }
    }

    void ShowInAngleOnly()
    {
        foreach (VisibleObject vo in Objects)
        {
            Vector3 vec = vo.transform.position - Character.transform.position;
            float dot = Vector3.Dot(vec.normalized, -Character.transform.forward);

            float angle = Mathf.Rad2Deg * Mathf.Acos(dot);

            if (angle < Angle)
                vo.Show();
            else
                vo.Hide();
        }
    }

    private void ShowAll()
    {
        foreach (VisibleObject vo in Objects)
        {
            vo.Show();
        }
    }

    void HideAll()
    {
        foreach(VisibleObject vo in Objects)
        {
            vo.Hide();
        }
    }
}
