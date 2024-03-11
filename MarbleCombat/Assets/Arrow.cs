using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

	public Vector2 target;
	public Vector2 shooter;
	public float scale;
	public int DMG;
    // Start is called before the first frame update
    void Start()
    {
		transform.position = shooter;
		target = target + (Vector2)transform.forward + Random.insideUnitCircle;
    }
	public ContactFilter2D filter;
    // Update is called once per frame
    void Update()
    {
		float remain = Vector2.Distance(transform.position, target);

		if (remain < 1) {
			//impact
			RaycastHit2D Hit = Physics2D.Raycast(transform.position, transform.up,2,filter.layerMask);
			//Debug.DrawRay(transform.position, transform.up * 2);
			if (Hit.collider != null) {
				Debug.Log("Collider");
				MarblePawn otherMB = Hit.collider.gameObject.GetComponentInChildren<MarblePawn>();
				Debug.Log("HIT!");
				if (otherMB) {
					otherMB.Damage(DMG,transform.up);
				}
			}
			Destroy(gameObject);
		} else {
			transform.up = (target - shooter).normalized;
			transform.position = Vector2.Lerp(transform.position, target, Time.deltaTime*2);
			float distance = Vector2.Distance(target, shooter)-1;
			float halfway = distance / 2;
			remain -= 1;
			scale = Mathf.Abs(remain-halfway) /halfway;
			transform.localScale = Vector3.Lerp(Vector3.one*2, Vector3.one, scale);
		}
    }
}
