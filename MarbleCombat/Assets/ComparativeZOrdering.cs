using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComparativeZOrdering : MonoBehaviour
{
	public Transform compare;
	SpriteRenderer sr;
	public int max = 4, min = 0;
    // Start is called before the first frame update
    void Start()
    {
		sr = GetComponent<SpriteRenderer>();
		if (compare == null)
			compare = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
		if ((compare.position - transform.position).y > 0) {
			sr.sortingOrder = max;
		} else sr.sortingOrder = min;
    }
}
