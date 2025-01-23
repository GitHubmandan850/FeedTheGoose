using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    public GameObject hungerBar;
    private float hunger = 100;
    public float food;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hunger -= Time.deltaTime;
    }
}
