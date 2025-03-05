using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temperature : MonoBehaviour
{
    public Text txt; 
    public Image hot;
    public Image cold;
    public Health health;
    public int temp;
    public float timer = 0;

    void Start()
    {
        temp = 70;
    }

    void Update()
    {
        timer += Time.deltaTime;
        txt.text = temp + " F";
        if(temp <= 32)
        {
           cold.color  = new Color(0f, 1f, 1f, (32f - temp) / 50f - .2f);
        }
        else
        {
            cold.color  = new Color(0f, 1f, 1f, 0f);
        }
        if(temp >= 100)
        {
            hot.color  = new Color(1f, 0f, 0f, (temp - 100) / 50f - .2f);
        }
        else
        {
            hot.color  = new Color(1f, 0f, 0f, 0f);;
        }

        if (temp <= 0)
        {
            health.Damage(Time.deltaTime * (0 - temp)); // Increased damage scaling
        }
        if (temp >= 132)
        {
            health.Damage(Time.deltaTime * (temp - 132)); // Increased damage scaling
        }

        if (temp > 70)
        {
            if (timer > 5)
            {
                timer -= .5f;
                temp -= 1;
            }
        }

        if (temp < 70)
        {
            if (timer > 5)
            {
                timer -= .25f;
                temp += 1;
            }
        }
    }

    public void Heat(int heat)
    {
        temp += heat;
        timer = 0;
    }
}
