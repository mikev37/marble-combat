using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
	Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		transform.up = Vector2.Lerp(transform.up, rb.velocity.normalized * -1, Time.deltaTime * 5);
	}
}
