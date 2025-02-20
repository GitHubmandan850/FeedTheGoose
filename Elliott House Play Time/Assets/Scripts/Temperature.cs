using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temperature : MonoBehaviour
{
    public Text txt; 
    public Image cold;
    public Image hot;
    public Health health;
    public int temp;

    void Start()
    {
        temp = 70;
    }

    void Update()
    {
        txt.text = temp + " F";
        if(temp <= 32)
        {
           cold.color = new Color(0, 255, 255, 32 - temp);
        }
        else
        {
            cold.color = new Color(0, 255, 255, 0);
        }
        if(temp >= 100)
        {
            hot.color = new Color(255, 255, 0, temp - 100);
        }
        else
        {
            hot.color = new Color(255, 255, 0, 0);;
        }

        if (temp <= 0)
        {
            health.Damage(Mathf.RoundToInt(Time.deltaTime * (0 - temp) * 5)); // Increased damage scaling
        }
        if (temp >= 132)
        {
            health.Damage(Mathf.RoundToInt(Time.deltaTime * (temp - 132) * 5)); // Increased damage scaling
        }
    }

    void Heat(int heat)
    {
        temp += heat;
    }
}
