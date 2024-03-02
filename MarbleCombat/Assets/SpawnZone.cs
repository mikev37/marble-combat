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
    void Start()
    {
		int startX = (int)transform.position.x - (columns / 2 * WIDTH);
		int rows = 0;
		while (num > 0) {
			for (int i = 0; i < columns; i++) {
				if(num > 0) {
					Vector3 start = new Vector3(startX + i * WIDTH, transform.position.y + rows * HEIGHT);
					GameObject go = GameObject.Instantiate(prefab, start, Quaternion.identity);
					go.GetComponent<MarblePawn>().key = key;
					go.GetComponentInChildren<Body>().GetComponent<SpriteRenderer>().sprite = body;
					num--;
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
