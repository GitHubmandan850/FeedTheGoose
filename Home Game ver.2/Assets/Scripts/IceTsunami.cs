using UnityEngine;

public class IceTsunami : MonoBehaviour
{
    	public GameObject icePrefab;
        public GameObject player;
    	public Transform goal;
    	public float riseSpeed = 1f;
    	public float moveSpeed = 5f;
    	public float scaleSpeed = 0.5f;
	    public Temperature temperature;
		public GameObject warningPrefab;
	
		private float timeNow;
    	private Vector3 direction;
    	private GameObject ice;
    	private Vector3 currentPos;
		private GameObject warning;

    	void Start()
    	{

            player = GameObject.FindWithTag("Player");
            GameObject controller = GameObject.FindWithTag("Manager");
            goal = player.transform;
            temperature = controller.GetComponent<Temperature>();
        	currentPos = transform.position;
	        direction = (goal.position - currentPos).normalized;

        	Quaternion lookRotation = Quaternion.LookRotation(direction);
	        ice = Instantiate(icePrefab, currentPos, lookRotation);
        	ice.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

			warning = Instantiate(warningPrefab, goal.position, warningPrefab.transform.rotation);

    	}

    	void Update()
    	{
			float delta = Time.deltaTime;
        	timeNow += delta;
        
        	if (timeNow > 5f)
            {
				Destroy(warning);
                if (timeNow <= 8f)
        	    {
            		currentPos.y += riseSpeed * delta;
            		

            		Vector3 scale = ice.transform.localScale;
            		scale += new Vector3(scaleSpeed * 0.5f, scaleSpeed * 2f, scaleSpeed * 0.5f) * delta;
            		ice.transform.localScale = scale;
                }
                else if (timeNow >= 15)
                {
                    currentPos.y -= riseSpeed * delta * 2;
                }
                currentPos += direction * moveSpeed * delta;
                ice.transform.position = currentPos;
                ice.transform.rotation = Quaternion.LookRotation(direction);
        	}

            

            if (ice.transform.position.y <= -10 && timeNow >= 10)
            {
                Destroy(ice);
                Destroy(gameObject);
            }
            else if (player.GetComponent<Collider>().bounds.Intersects(ice.GetComponent<Collider>().bounds))
            {
                temperature.Heat(-1);
            }
    	}
}
