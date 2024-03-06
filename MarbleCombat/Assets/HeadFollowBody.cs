using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollowBody : MonoBehaviour
{
    public Transform body;
	public bool rotate;
	Vector2 diff;
    // Start is called before the first frame update
    void Start()
    {

		diff = gameObject.transform.position - body.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		Quaternion rotation = transform.rotation;
		if (rotate)
			rotation = body.rotation;
		gameObject.transform.SetPositionAndRotation(body.position + (Vector3)diff, rotation);
    }
}
