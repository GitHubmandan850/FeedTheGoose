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
        num = Random.Range(0, 10000);

        //spawns a fire storm
        if(num == 420 || num == 24)
        {
            GameObject newFireStorm = Instantiate(fireStorm, GenerateSpawnPosition(1), fireStorm.transform.rotation);
            newFireStorm.GetComponent<FireShower>().player = player;
            newFireStorm.GetComponent<FireShower>().temperature = temperature;
        }

        //spawns an ice tsunami
        if(num == 6969 || num == 9696)
        {
            GameObject newIceTsunami = Instantiate(iceTsunami, GenerateSpawnPosition(4), frostStorm.transform.rotation);
            newIceTsunami.GetComponent<IceTsunami>().player = player;
            newIceTsunami.GetComponent<IceTsunami>().temperature = temperature;
        }

        //spawns an frost cloud
        if(num == 69 || num == 96)
        {
            GameObject newFrostStorm = Instantiate(frostStorm, GenerateSpawnPosition(1), frostStorm.transform.rotation);
            newFrostStorm.GetComponent<FrostStorm>().player = player;
            newFrostStorm.GetComponent<FrostStorm>().temperature = temperature;
        }

        //spawns a firenado
        if(num == 4242 || num == 2424)
        {
            GameObject newFrostStorm = Instantiate(frostStorm, GenerateSpawnPosition(1), frostStorm.transform.rotation);
            newFrostStorm.GetComponent<FrostStorm>().player = player;
            newFrostStorm.GetComponent<FrostStorm>().temperature = temperature;
        }

        //spawns a degrandew storm
        if(num == 6699 || num == 9966)
        {
            GameObject newFrostStorm = Instantiate(frostStorm, GenerateSpawnPosition(0, 0, 0, 1), frostStorm.transform.rotation);
            newFrostStorm.GetComponent<FrostStorm>().player = player;
            newFrostStorm.GetComponent<FrostStorm>().temperature = temperature;
        }
    }


    //this generates a spawn position between -20 and 20 multiplied by the mult
    private Vector3 GenerateSpawnPosition(int x, int y, int z, int mult)
    {
        Vector3 objectPosition = new Vector3(x, y, z);
        Vector3 minBounds = new Vector3(objectPosition.x - 20 * mult, objectPosition.y, objectPosition.z - 20 * mult);
        Vector3 maxBounds = new Vector3(objectPosition.x + 21 * mult, objectPosition.y, objectPosition.z + 21 * mult);

        float spawnPosX = Random.Range(minBounds.x, maxBounds.x);
        float spawnPosZ = Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(spawnPosX, 0, spawnPosZ);
    }
}
