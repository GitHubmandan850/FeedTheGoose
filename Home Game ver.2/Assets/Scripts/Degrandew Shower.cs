using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for Image

public class DegrandewShower : MonoBehaviour
{
    public GameObject player;
    public GameObject cloudPrefab;
    public GameObject degrandewPrefab;
    public Image degran1;
    public Image degran2;
    public Image degran3;
    public Image degran4;
    public GameObject warningPrefab;
    public Temperature temperature;
    
    private float timer;
    private List<GameObject> spawnedDegrandews = new List<GameObject>(); // Track instances
    private bool degrandew = false;
    private int movemementX;
    private int movemementZ;
    private List<GameObject> clouds = new List<GameObject>();
    private GameObject warning;
    private Image degran;
    private float time;
    private int count = 0;
    private Color color;

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

        degran = degran1;

        //This chooses what direction the storm will move in
        movemementX = Random.Range(-1, 2);
        movemementZ = Random.Range(-1, 2);
    }

    void Update()
    {
        int num = Random.Range(0, 4);
        if(num == 0)
        {
            degran = degran1;
        }
        else if(num == 1)
        {
            degran = degran2;
        }
        else if(num == 2)
        {
            degran = degran3;
        }
        else
        {
            degran = degran4;
        }
        //Saves change in time and updates the timer
        time = Time.deltaTime;
        timer += time;

        color = degran.color;
        color.a = Mathf.Max(0f, color.a - 0.05f);
        degran.color = color;

        //After the 5 second warning is up
        if(degrandew)
        {
            //Moves the storm 
            transform.position = new Vector3(transform.position.x + movemementX * time, transform.position.y, transform.position.z + movemementZ * time);
            for (int i = clouds.Count - 1; i >= 0; i--)
            {
                clouds[i].transform.position = new Vector3(clouds[i].transform.position.x + movemementX * time, clouds[i].transform.position.y, clouds[i].transform.position.z + movemementZ * time);
            }

            //Spawns 50 degrandew per second
            if (timer >= 0.10)
            {
                timer = 0;
                SpawnDegrandew(5);
                count += 1;

                //The storm dies after spawning 500 degrandews
                if(count >= 100)
                {
                    //Destroy the clouds
                    for (int i = clouds.Count - 1; i >= 0; i--)
                    {
                        Destroy(clouds[i]);
                    }
                    //Destory all remaining degrandews
                    for (int i = spawnedDegrandews.Count - 1; i >= 0; i--)
                    {
                        Destroy(spawnedDegrandews[i]);
                    }
                    color = degran.color;
                    color.a = Mathf.Max(0f, 0f);
                    degran.color = color;
                    //Destroy the storm
                    Destroy(gameObject);
                }
            }

            //Checks each degrandew 
            for (int i = spawnedDegrandews.Count - 1; i >= 0; i--) 
            {
                //If it is under the map it gets destroyed
                if (spawnedDegrandews[i].transform.position.y < 1)
                {
                    Destroy(spawnedDegrandews[i]);
                    spawnedDegrandews.RemoveAt(i);
                }

                //If it hits the player it gets destroyed and the player is heated up by 3
                else if (player.GetComponent<Collider>().bounds.Intersects(spawnedDegrandews[i].GetComponent<Collider>().bounds))
                {
                    Destroy(spawnedDegrandews[i]);
                    spawnedDegrandews.RemoveAt(i);
                    color = degran.color;
                    color.a = Mathf.Max(0f, 1.5f);
                    degran.color = color;
                }
            }
        }
        //For the first 5 seconds there is no storm
        if(timer < 5f && !degrandew)
        {
            degrandew = false;
        }
        //After the first 5 seconds the storm starts and the warning is destroyed
        else
        {
            degrandew = true;
            Destroy(warning);
        }
    }

    //This will spawn the degrandews
    void SpawnDegrandew(int num)
    {
        //This adds num degrandews to our degrandew list
        for (int i = 0; i < num; i++)
        {
            GameObject newDegrandew = Instantiate(degrandewPrefab, GenerateSpawnPosition(), degrandewPrefab.transform.rotation);
            newDegrandew.GetComponent<Rigidbody>().AddForce(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-1, 5), ForceMode.Impulse);
            spawnedDegrandews.Add(newDegrandew);
        }
    }

    //This generates the spawn position for our clouds and degrandew
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
