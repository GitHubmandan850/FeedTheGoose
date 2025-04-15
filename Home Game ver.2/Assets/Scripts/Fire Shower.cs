using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShower : MonoBehaviour
{
    public GameObject firePrefab;
    public GameObject player;
    public Temperature temperature;
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
