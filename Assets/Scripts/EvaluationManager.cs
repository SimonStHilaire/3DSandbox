using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvaluationManager : MonoBehaviour
{
    public Material ControlledMaterial;
    public Slider MaterialSlider;

    public Transform Earth;
    public Transform Moon;
    public Transform Mars;

    public float EarthSpeed;

    public Text MoonMarsDistanceText;
    public Text MoonCamrFAngleText;

    public Transform Rock;
    public float RockSpeed;

    public void OnSliderChanged()
    {
        //TODO:
        //ControlledMaterial
    }

    public void Update()
    {
        Earth.Rotate(new Vector3(0f, Time.deltaTime * EarthSpeed, 0f));
        
        //TODO: Distance Moon-mars
        float distance = 0f;
        MoonMarsDistanceText.text = distance.ToString();

        //TODO: Angle: Moon / Camera Forward
        float angle = 0f;
        MoonCamrFAngleText.text = angle.ToString();

        //TODO: Update Rock
        //Rock

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //TODO: Afficher la normale créée par le vecteur up (0,1,0) et le vecteur entre "Moon" et "Earth"
        //Gizmos.DrawLine(from, to, Color.red);
    }
#endif //UNITY_EDITOR
}
