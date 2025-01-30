using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthBar;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
    }

    public void Damage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            //death
            health = 0;
        }
    }

    public void Heal(int heal)
    {
        health += heal;

        if(health > 10)
        {
            health = 10;
        }
    }
}
