using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drinking : MonoBehaviour
{
    public Slider thirstBar;
    public Health Health;
    public float thirst;
    public float decline = 1;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        thirst = 200;
    }

    // Update is called once per frame
    void Update()
    {
        thirstBar.value = thirst;
        thirst -= 5 * Time.deltaTime;

        if(thirst <= 0)
        {
            thirst = 0;
            timer += Time.deltaTime;
        }

        if(timer >= 1.5)
        {
            timer = 0;
            Health.Damage(1);
        }
    }

    public void Drink(int drink)
    {
        thirst += drink;
        if(thirst > 200)
        {
            thirst = 200;
        }
    }

    public void Thirst(int drink)
    {
        decline = drink;
    }
}
