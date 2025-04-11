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
    public float timer;
    private List<GameObject> spawnedFires = new List<GameObject>(); // Track instances
    private bool fire = false;
    public int movemementX;
    public int movemementZ;
    private GameObject cloud;
    private GameObject warning;
    float time;

    void Start()
    {
        cloud = Instantiate(cloudPrefab, GenerateSpawnPosition(), cloudPrefab.transform.rotation);
        warning = Instantiate(warningPrefab, transform.position, warningPrefab.transform.rotation);
        movemementX = Random.Range(-1, 1);
        movemementZ = Random.Range(-1, 1);
    }
    void Update()
    {
        time = Time.deltaTime;
        timer += time;
        if(fire)
        {
            transform.position = new Vector3(transform.position.x + movemementX * time, transform.position.y, transform.position.z + movemementZ * time);
            cloud.transform.position  = new Vector3(cloud.transform.position.x + movemementX * time, cloud.transform.position.y, cloud.transform.position.z  + movemementZ * time);
            if (timer >= 0.10f)
            {
                timer = 0f;
                SpawnFire(5);
            }

            for (int i = spawnedFires.Count - 1; i >= 0; i--) 
            {
                if (spawnedFires[i].transform.position.y < transform.position.y + 1)
                {
                    Destroy(spawnedFires[i]);
                    spawnedFires.RemoveAt(i);
                }

                else if (player.GetComponent<Collider>().bounds.Intersects(spawnedFires[i].GetComponent<Collider>().bounds))
                {
                    Destroy(spawnedFires[i]);
                    spawnedFires.RemoveAt(i);
                    temperature.Heat(3);
                }
            }
        }
        if(timer < 5f && !fire)
        {
            fire = false;
        }
        else
        {
            fire = true;
            Destroy(warning);
        }
    }

    void SpawnFire(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject newFire = Instantiate(firePrefab, GenerateSpawnPosition(), firePrefab.transform.rotation);
            newFire.GetComponent<Rigidbody>().AddForce(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-1, 5), ForceMode.Impulse);
            spawnedFires.Add(newFire);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 objectPosition = transform.position;
        Vector3 minBounds = new Vector3(objectPosition.x - 7, objectPosition.y, objectPosition.z - 7);
        Vector3 maxBounds = new Vector3(objectPosition.x + 7, objectPosition.y + 25, objectPosition.z + 7);

        float spawnPosX = Random.Range(minBounds.x, maxBounds.x);
        float spawnPosZ = Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(spawnPosX, 25, spawnPosZ);
    }
}
