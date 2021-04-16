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
        //GameObject newObj = new GameObject("New quad mesh");

        //Filter
        //Renderer
        //Material

        //Vertices
        //triangles indexes
        //Normals
        //*uv
    }

    void ShowFrontOnly()
    {
        //Use Dot product
    }

    void ShowInAngleOnly()
    {
        //Use Dot product
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
