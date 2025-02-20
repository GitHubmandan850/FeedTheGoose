using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName ="Scriptables/Lighting Preset",order =1)]
public class Light_condition_preset : ScriptableObject
{
    public Gradient AmbeintColor;
    public Gradient DirectionalColor;
    public Gradient Fogcolor;
}
