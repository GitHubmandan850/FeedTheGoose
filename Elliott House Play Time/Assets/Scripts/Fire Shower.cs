using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShower : MonoBehaviour
{
    public GameObject firePrefab;
    public double timer;
    private List<GameObject> spawnedFires = new List<GameObject>(); // Track instances

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.10)
        {
            timer = 0;
            SpawnFire(5);
        }

        for (int i = spawnedFires.Count - 1; i >= 0; i--) 
        {
            if (spawnedFires[i].transform.position.y < 1)
            {
                Destroy(spawnedFires[i]);
                spawnedFires.RemoveAt(i);
            }
        }
    }

    void SpawnFire(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject newFire = Instantiate(firePrefab, GenerateSpawnPosition(), firePrefab.transform.rotation);
            spawnedFires.Add(newFire);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 objectPosition = transform.position;
        Vector3 minBounds = new Vector3(objectPosition.x - 5, objectPosition.y, objectPosition.z - 5);
        Vector3 maxBounds = new Vector3(objectPosition.x + 5, objectPosition.y + 25, objectPosition.z + 5);

        float spawnPosX = Random.Range(minBounds.x, maxBounds.x);
        float spawnPosZ = Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(spawnPosX, 25, spawnPosZ);
    }
}
