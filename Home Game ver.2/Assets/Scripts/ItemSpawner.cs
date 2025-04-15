using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject wood;
    public GameObject rock;
    public GameObject metal;
    public int type;
    public int sizeX;
    public int sizeY;
    public int sizeZ;
    public float spawnCooldown = 30f;
    private float lastSpawnTime = 0f;

    void Update()
    {
        // Only try to spawn if cooldown is over
        if (Time.time >= lastSpawnTime + spawnCooldown)
        {
            // Try to spawn with 0.1% chance
            if (Random.value < 0.01f)
            {
                GameObject toSpawn = null;

                if (type == 0)
                {
                    toSpawn = wood;
                }
                else if (type == 1)
                {
                    toSpawn = rock;
                }
                else if (type == 2)
                {
                    toSpawn = metal;
                }

                if (toSpawn != null)
                {
                    Instantiate(toSpawn, generateSpawnPosition(), toSpawn.transform.rotation);
                    lastSpawnTime = Time.time;
                }
            }
        }
    }

    private Vector3 generateSpawnPosition()
    {
        float x = Random.Range(transform.position.x - sizeX, transform.position.x + sizeX);
        float y = Random.Range(transform.position.y - sizeY, transform.position.y + sizeY);
        float z = Random.Range(transform.position.z - sizeZ, transform.position.z + sizeZ);
        return new Vector3(x, y, z);
    }


}
