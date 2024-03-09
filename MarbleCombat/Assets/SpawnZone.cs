using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{

	public int columns, num;
	public GameObject prefab;
	public int key;
	public Sprite body;
	public int WIDTH, HEIGHT = 1;
	// Start is called before the first frame update
	List<GameObject> goose;
    void Start()
    {
		goose = new List<GameObject>();
		int startX = (int)transform.position.x - (columns / 2 * WIDTH);
		int rows = 0;
		int iterator = 0;
		while (num > 0) {
			for (int i = 0; i < columns; i++) {
				if(num > 0) {
					Vector3 start = new Vector3(startX + i * WIDTH, transform.position.y + rows * HEIGHT);
					GameObject go = GameObject.Instantiate(prefab, start, Quaternion.identity);
					go.GetComponent<MarblePawn>().key = key;
					go.GetComponentInChildren<Body>().GetComponent<SpriteRenderer>().sprite = body;
					if (iterator % columns > 0) {
						/*SpringJoint2D join = go.AddComponent<SpringJoint2D>();
						join.connectedBody = goose[iterator - 1].GetComponent<Rigidbody2D>();
						join.enableCollision = true;
						/*join.maxForce = 3;
						join.maxTorque = .1f;
						join.autoConfigureOffset = true;
						join.distance = WIDTH;
						join.frequency = .5f;
						join.dampingRatio = .1f;
						join.breakForce = 100;*/
						RelativeJoint2D join2 = go.AddComponent<RelativeJoint2D>();
						join2.enableCollision = true;
						join2.connectedBody = goose[iterator - 1].GetComponent<Rigidbody2D>();
						join2.maxForce = 1;
						join2.maxTorque = 0.001f;
						//join2.maxTorque = .01f;
						join2.autoConfigureOffset = true;
					}
					if (rows > 0) {
						/*SpringJoint2D join2 = go.AddComponent<SpringJoint2D>();
						join2.enableCollision = true;
						join2.connectedBody = goose[iterator - columns].GetComponent<Rigidbody2D>();
						join2.distance = HEIGHT;
						join2.frequency = .5f;
						join2.dampingRatio = .1f;
						join2.breakForce = 100;*/
						RelativeJoint2D join2 = go.AddComponent<RelativeJoint2D>();
						join2.enableCollision = true;
						join2.connectedBody = goose[iterator - columns].GetComponent<Rigidbody2D>();
						join2.maxForce = 1;
						join2.maxTorque = 0.001f;
						//join2.maxTorque = .01f;
						join2.autoConfigureOffset = true;
						/*join2.breakForce = 10;*/
					}
					goose.Add(go);
					num--;
					iterator++;
				}
			}
			rows++;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
