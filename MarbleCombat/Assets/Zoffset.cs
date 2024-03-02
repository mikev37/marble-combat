using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoffset : MonoBehaviour
{
	public float heightOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		heightOffset = Mathf.Lerp(heightOffset, 0, Time.deltaTime * .1f);
		transform.position = transform.parent.position + Vector3.up * heightOffset;
    }
}
