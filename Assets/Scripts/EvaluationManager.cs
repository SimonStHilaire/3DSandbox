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

    public void OnSliderChanged(float x)
    {
        ControlledMaterial.SetFloat("_factor", x);
    }

    public void Update()
    {
        Earth.Rotate(new Vector3(0f, Time.deltaTime * EarthSpeed, 0f));

        //TODO: Distance Moon-mars
        float distance = Vector3.Distance(Moon.position, Mars.position);
        MoonMarsDistanceText.text = distance.ToString();

        //TODO: Angle: Moon / Camera Forward
        Vector3 targetDir = Moon.position - Camera.main.transform.position;
        float angle = Vector3.Angle(targetDir, Camera.main.transform.forward);
        MoonCamrFAngleText.text = angle.ToString();

        //TODO: Update Rock
        //Rock
        Rock.position = Vector3.Lerp(Rock.position, Moon.position, RockSpeed);

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 moonEarth = Earth.position - Moon.position;
        Vector3 cross = Vector3.Cross(Vector3.up, moonEarth).normalized;

        //TODO: Afficher la normale créée par le vecteur up (0,1,0) et le vecteur entre "Moon" et "Earth"
        Gizmos.color = Color.red;
        Gizmos.DrawLine(cross, Vector3.up);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Earth.position, Earth.position + cross);
    }
#endif //UNITY_EDITOR
}
