using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPawn : MarblePawn
{
	public Arrow arrow;
	public int MIN_DISTANCE, MAX_DISTANCE, RELOAD;

	void Start() {
		ATTACK_DISTANCE = MAX_DISTANCE;
		GetComponentInChildren<Rigidbody2D>().mass = MASS;
		shoot = RELOAD;
	}
	float shoot = 1;
	// Update is called once per frame
	protected override void Charge(Transform target) {
		// find distance between object and target
		Vector2 targetDelta = target.position - transform.position;

		//get the angle between transform.up and target delta
		float angleDiff = Vector2.SignedAngle(transform.up * -1, targetDelta);
		float turn = angleDiff;
		if (angleDiff > 1)
			turn = 1;
		else if (angleDiff < -1)
			turn = -1;
		// apply torque along that axis according to the magnitude of the angle.
		GetComponent<Rigidbody2D>().AddTorque(turn * TURN);

		if (Vector2.Angle(transform.up * -1, targetDelta) < 25) {
			eyes.sprite = charge;

			if (Vector2.Distance(target.position, transform.position) < MIN_DISTANCE) {
				float MOV = -1 * STR / 2f;
				GetComponent<Rigidbody2D>().AddForce(targetDelta.normalized * MOV);
			} 
			if (shoot > 0) {
				shoot -= Time.deltaTime;
			} else {
				GameObject go = GameObject.Instantiate(arrow.gameObject);
				Arrow a = go.GetComponent<Arrow>();
				a.shooter = transform.position;
				a.target = target.position;
				shoot = RELOAD;
			}
			
		} else {
			shoot = RELOAD;
		}

	}
}
