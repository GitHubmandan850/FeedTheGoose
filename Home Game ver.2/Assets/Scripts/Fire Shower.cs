using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShower : MonoBehaviour
{
    public GameObject firePrefab;
    public GameObject player;
    public GameObject cloudPrefab;
    public GameObject warningPrefab;
    public Temperature temperature;
    
    private float timer;
    private List<GameObject> spawnedFires = new List<GameObject>(); // Track instances
    private bool fire = false;
    private int movemementX;
    private int movemementZ;
    private List<GameObject> clouds = new List<GameObject>();
    private GameObject warning;
    private float time;
    private int count = 0;

    void Start()
    {
        //Finds the player and temperature
        player = GameObject.FindWithTag("Player");
        GameObject controller = GameObject.FindWithTag("Manager");
        temperature = controller.GetComponent<Temperature>();

        //Makes the cloud that follows the storm
        for (int i = 0; i < 30; i++)
        {
            GameObject newCloud = Instantiate(cloudPrefab, GenerateSpawnPosition(), cloudPrefab.transform.rotation);
            clouds.Add(newCloud);
        }

        //Makes the warning sign that notifies players a storm will spawn
        warning = Instantiate(warningPrefab, transform.position, warningPrefab.transform.rotation);

        //This chooses what direction the storm will move in
        movemementX = Random.Range(-1, 2);
        movemementZ = Random.Range(-1, 2);
    }

    void Update()
    {
        //Saves change in time and updates the timer
        time = Time.deltaTime;
        timer += time;

        //After the 5 second warning is up
        if(fire)
        {
            //Moves the storm 
            transform.position = new Vector3(transform.position.x + movemementX * time, transform.position.y, transform.position.z + movemementZ * time);
            for (int i = clouds.Count - 1; i >= 0; i--)
            {
                clouds[i].transform.position = new Vector3(clouds[i].transform.position.x + movemementX * time, clouds[i].transform.position.y, clouds[i].transform.position.z + movemementZ * time);
            }

            //Spawns 50 fire per second
            if (timer >= 0.10)
            {
                timer = 0;
                SpawnFire(5);
                count += 1;

                //The storm dies after spawning 500 fires
                if(count >= 100)
                {
                    //Destroy the clouds
                    for (int i = clouds.Count - 1; i >= 0; i--)
                    {
                        Destroy(clouds[i]);
                    }
                    //Destory all remaining fires
                    for (int i = spawnedFires.Count - 1; i >= 0; i--)
                    {
                        Destroy(spawnedFires[i]);
                    }
                    //Destroy the storm
                    Destroy(gameObject);
                }
            }

            //Checks each fire 
            for (int i = spawnedFires.Count - 1; i >= 0; i--) 
            {
                //If it is under the map it gets destroyed
                if (spawnedFires[i].transform.position.y < 1)
                {
                    Destroy(spawnedFires[i]);
                    spawnedFires.RemoveAt(i);
                }

                //If it hits the player it gets destroyed and the player is heated up by 3
                else if (player.GetComponent<Collider>().bounds.Intersects(spawnedFires[i].GetComponent<Collider>().bounds))
                {
                    Destroy(spawnedFires[i]);
                    spawnedFires.RemoveAt(i);
                    temperature.Heat(3);
                }
            }
        }
        //For the first 5 seconds there is no storm
        if(timer < 5f && !fire)
        {
            fire = false;
        }
        //After the first 5 seconds the storm starts and the warning is destroyed
        else
        {
            fire = true;
            Destroy(warning);
        }
    }

    //This will spawn the fires
    void SpawnFire(int num)
    {
        //This adds num fires to our fire list
        for (int i = 0; i < num; i++)
        {
            GameObject newFire = Instantiate(firePrefab, GenerateSpawnPosition(), firePrefab.transform.rotation);
            newFire.GetComponent<Rigidbody>().AddForce(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-1, 5), ForceMode.Impulse);
            spawnedFires.Add(newFire);
        }
    }

    //This generates the spawn position for our clouds and fire
    private Vector3 GenerateSpawnPosition()
    {
        Vector3 objectPosition = transform.position;
        Vector3 minBounds = new Vector3(objectPosition.x - 7, objectPosition.y + 23, objectPosition.z - 7);
        Vector3 maxBounds = new Vector3(objectPosition.x + 7, objectPosition.y + 26, objectPosition.z + 7);

        float spawnPosX = Random.Range(minBounds.x, maxBounds.x);
        float spawnPosY = Random.Range(minBounds.y, maxBounds.y);
        float spawnPosZ = Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(spawnPosX, 25, spawnPosZ);
    }
}
