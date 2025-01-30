using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eating : MonoBehaviour
{
    public Slider hungerBar;
    public Health Health;
    public float hunger;
    public float decline = 1;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        hunger = 100;
    }

    // Update is called once per frame
    void Update()
    {
        hungerBar.value = hunger;
        hunger -= 5 * Time.deltaTime;

        if(hunger <= 0)
        {
            hunger = 0;
            timer += Time.deltaTime;
        }

        if(timer >= 1.5)
        {
            timer = 0;
            Health.Damage(1);
        }
    }

    public void Eat(int food)
    {
        hunger += food;
        if(hunger > 100)
        {
            hunger = 100;
        }
    }

    public void Hunger(int food)
    {
        decline = food;
    }
}
