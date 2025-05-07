using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNado : MonoBehaviour
{
	private float time;
	private bool start = false;
	private GameObject warning;
	public GameObject layer1;
	public GameObject layer2;
	public GameObject layer3;
	public GameObject layer4;
	public GameObject layer5;
	public GameObject firePrefab;
	public GameObject player;
    public GameObject warningPrefab;
    public Temperature temperature;

	private List<GameObject> spawnedFires = new List<GameObject>();

    	void Start()
    	{
			//Finds the player and temperature
			player = GameObject.FindWithTag("Player");
			GameObject controller = GameObject.FindWithTag("Manager");
			temperature = controller.GetComponent<Temperature>();

			//Makes the warning sign that notifies players a storm will spawn
        	warning = Instantiate(warningPrefab, transform.position, warningPrefab.transform.rotation);
        	
    	}

    	void Update()
    	{

			time += Time.deltaTime;
			if(time >= 5 && !start)
			{
				Destroy(warning);

				SpawnFires(spawnedFires, layer1, 30);
				SpawnFires(spawnedFires, layer2, 30);
				SpawnFires(spawnedFires, layer3, 30);
				SpawnFires(spawnedFires, layer4, 30);
				SpawnFires(spawnedFires, layer5, 30);

				start = true;
			}
			if(start)
			{
				//Checks each fire 
				for (int i = spawnedFires.Count - 1; i >= 0; i--) 
				{
					//If it hits the player it gets destroyed and the player is heated up by 10
					if (player.GetComponent<Collider>().bounds.Intersects(spawnedFires[i].GetComponent<Collider>().bounds))
					{
						Destroy(spawnedFires[i]);
						spawnedFires.RemoveAt(i);
						temperature.Heat(10);
					}
				}
			}
			if(time >= 20)
			{
				for (int i = spawnedFires.Count - 1; i >= 0; i--) 
				{
					Destroy(spawnedFires[i]);
				}
				Destroy(gameObject);
			}
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

