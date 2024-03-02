using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hill : MonoBehaviour
{
	public float push;
	float heightoffset = .5f, radius;
	// Start is called before the first frame update
	void Start()
    {
		radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerStay2D(Collider2D collision) {
		MarblePawn otherMB = collision.gameObject.GetComponentInChildren<MarblePawn>();
		if (otherMB) {

			Vector3 facing = otherMB.transform.up;
			Vector3 toTarget = (otherMB.transform.position - transform.position).normalized;
			float dot = Mathf.Abs(Vector2.Dot(facing, toTarget));
						otherMB.GetComponent<Rigidbody2D>().AddForce(dot * (otherMB.transform.position - transform.position).normalized * push,ForceMode2D.Force);
		}
		Zoffset otherZO = collision.gameObject.GetComponentInChildren<Zoffset>();
		if (otherZO) {
			if ((radius - Vector2.Distance(otherZO.transform.position, transform.position)) / radius > 0)
				otherZO.GetComponentInChildren<Zoffset>().heightOffset = (radius - Vector2.Distance(otherZO.transform.position, transform.position)) / radius * push * heightoffset;
		}
	}
}
