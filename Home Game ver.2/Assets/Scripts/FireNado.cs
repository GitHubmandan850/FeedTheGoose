using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNado : MonoBehaviour
{
	public GameObject layer1;
	public GameObject layer2;
	public GameObject layer3;
	public GameObject layer4;
	public GameObject layer5;
	public GameObject firePrefab;

	private List<GameObject> spawnedFires = new List<GameObject>();

    	void Start()
    	{
        	SpawnFires(spawnedFires, layer1, 15);
        	SpawnFires(spawnedFires, layer2, 15);
	        SpawnFires(spawnedFires, layer3, 15);
        	SpawnFires(spawnedFires, layer4, 15);
        	SpawnFires(spawnedFires, layer5, 15);
    	}

    	void Update()
    	{
        	
    	}

	private void SpawnFires(List<GameObject> list, GameObject layer, int num)
	{
		for(int i = 0; i < num; i++)
		{
			Vector3 spawnPos = layer.transform.position + GenerateSpawnPosition();
            		Quaternion rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

            		GameObject fire = Instantiate(firePrefab, spawnPos, rotation, layer.transform);
            		list.Add(fire);
		}
	}

	private Vector3 GenerateSpawnPosition()
	{
		float mult = Random.Range(1f, 5f);
		return new Vector3(Random.Range(-mult / 2, mult / 2), mult, Random.Range(-mult / 2, mult / 2));
	}

}

