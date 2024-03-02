using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSensor : MonoBehaviour
{
	MarblePawn mp;
	CircleCollider2D sensor;
	int desiredRadius = 5;
	int MIN = 3, MAX = 16;
    // Start is called before the first frame update
    void Start()
    {
		sensor = GetComponent<CircleCollider2D>();
		mp = GetComponentInParent<MarblePawn>();
		
    }

    // Update is called once per frame
    void Update()
    {
		int totalContacts = mp.allies.Count + mp.enemies.Count;
		desiredRadius = Mathf.Max(MIN, MAX - (int)Mathf.Pow(totalContacts,2));
    }

	private void FixedUpdate() {
		//sensor.radius = Mathf.Lerp(sensor.radius, desiredRadius, Time.fixedDeltaTime*.1f);
	}
}
