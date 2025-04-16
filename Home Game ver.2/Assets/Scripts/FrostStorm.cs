using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostStorm : MonoBehaviour
{
    public GameObject cloudPrefab;
    public GameObject player;
    public Temperature temperature;
    public float timer;
    public GameObject warningPrefab;

    private List<GameObject> spawnedClouds = new List<GameObject>(); // Track instances
    private GameObject warning;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        GameObject controller = GameObject.FindWithTag("Manager");
        temperature = controller.GetComponent<Temperature>();
        warning = Instantiate(warningPrefab, transform.position, warningPrefab.transform.rotation);
        SpawnClouds(125);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(Time.time >= 5)
        {
            for (int i = spawnedClouds.Count - 1; i >= 0; i--) 
            {
                if (player.GetComponent<Collider>().bounds.Intersects(spawnedClouds[i].GetComponent<Collider>().bounds))
                {
                    Destroy(spawnedClouds[i]);
                    spawnedClouds.RemoveAt(i);
                    temperature.Heat(-3);
                }
            }
        }
        if(Time.time >= 20)
        {
            for (int i = spawnedClouds.Count - 1; i >= 0; i--) 
            {
                Destroy(spawnedClouds[i]);
            }
            Destroy(gameObject);
        }
    }

    void SpawnClouds(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject newCloud = Instantiate(cloudPrefab, GenerateSpawnPosition(), cloudPrefab.transform.rotation);
            spawnedClouds.Add(newCloud);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 objectPosition = transform.position;
        Vector3 minBounds = new Vector3(objectPosition.x - 3, objectPosition.y, objectPosition.z - 3);
        Vector3 maxBounds = new Vector3(objectPosition.x + 3, objectPosition.y + 2, objectPosition.z + 3);

        float spawnPosX = Random.Range(minBounds.x, maxBounds.x);
        float spawnPosY = Random.Range(minBounds.y, maxBounds.y);
        float spawnPosZ = Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(spawnPosX, spawnPosY, spawnPosZ);
    }
}
