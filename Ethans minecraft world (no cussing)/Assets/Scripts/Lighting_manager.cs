using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Lighting_manager : MonoBehaviour
{
    //References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private Light_condition_preset Preset;
    //Variables
    [SerializeField, Range(0,24)] private float TimeOfDay;


    private void UpdateLighting(float timePercent)
    {

        RenderSettings.ambeintLight = Preset.AmbeintColor.Evaluate(timePercent);
        RenderSettings.Fogcolor = Preset.Fogcolor.Evaluate(timePercent);

        if(DirectionalLight!=null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.locationRotation = Quaternion.Euler(new Vector3((timePercent * 360f)-90f, 170, 0));
        }


    }


    //Try to find a directional light to use if we havn't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if(RenderSettings.sun!=null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
