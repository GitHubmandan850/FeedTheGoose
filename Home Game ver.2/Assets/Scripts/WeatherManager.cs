using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public float num = 0;
    public GameObject fireStorm;
    public GameObject frostStorm;
    public GameObject iceTsunami;
    public GameObject player;
    public Temperature temperature;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gets the random number
        num = Random.Range(0, 7000);

        //spawns a fire storm
        if(num == 420)
        {
            GameObject newFireStorm = Instantiate(fireStorm, GenerateSpawnPosition(1), fireStorm.transform.rotation);
            newFireStorm.GetComponent<FireShower>().player = player;
            newFireStorm.GetComponent<FireShower>().temperature = temperature;
        }

        //spawns an ice tsunami
        if(num == 6969)
        {
            GameObject newIceTsunami = Instantiate(iceTsunami, GenerateSpawnPosition(4), frostStorm.transform.rotation);
            newIceTsunami.GetComponent<IceTsunami>().player = player;
            newIceTsunami.GetComponent<IceTsunami>().temperature = temperature;
        }

        //spawns an frost cloud
        if(num == 69)
        {
            GameObject newFrostStorm = Instantiate(frostStorm, GenerateSpawnPosition(1), frostStorm.transform.rotation);
            newFrostStorm.GetComponent<FrostStorm>().player = player;
            newFrostStorm.GetComponent<FrostStorm>().temperature = temperature;
        }
    }


    //this generates a spawn position between -20 and 20 multiplied by the mult
    private Vector3 GenerateSpawnPosition(int mult)
    {
        Vector3 objectPosition = new Vector3(0, 0, 0);
        Vector3 minBounds = new Vector3(objectPosition.x - 20 * mult, objectPosition.y, objectPosition.z - 20 * mult);
        Vector3 maxBounds = new Vector3(objectPosition.x + 21 * mult, objectPosition.y, objectPosition.z + 21 * mult);

        float spawnPosX = Random.Range(minBounds.x, maxBounds.x);
        float spawnPosZ = Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(spawnPosX, 0, spawnPosZ);
    }
}
