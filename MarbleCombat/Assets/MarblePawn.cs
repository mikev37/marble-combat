using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarblePawn : MonoBehaviour
{
	public Rigidbody2D marble;
	public SpriteRenderer eyes;
	public Sprite charge, stunned, normal, flee;
	public int key;
	public int STR, MASS, DEF, HP, SHLD, ATK, MRL,TURN;
	public double STUN;
	private int SEPARATE_DISTANCE = 1;
	private int CHARGE_DISTANCE = 2;
	protected int ATTACK_DISTANCE = 5;
	// Start is called before the first frame update
	void Start()
    {
		
		GetComponentInChildren<Rigidbody2D>().mass = MASS;   
    }
	float update;
	protected Transform closestEnemy;
    // Update is called once per frame
    void Update()
    {
        if(STUN > 0) {
			STUN -= Time.deltaTime;
			Stun();
		} else {
			if (update <= 0) {
				findClosestEnemy();
				update = Random.value + .2f;
			} else update -= Time.deltaTime;
			if(closestEnemy && Vector2.Distance(closestEnemy.position,transform.position) < ATTACK_DISTANCE) {
				Charge(closestEnemy);
			} else {
				Normal();
			}
		}
    }

	protected virtual void findClosestEnemy() {
		enemies.RemoveAll(s => s == null);
		closestEnemy = null;
		foreach (MarblePawn mb in enemies) {
			if (!closestEnemy || Vector2.Distance(closestEnemy.position, transform.position) > Vector2.Distance(mb.transform.position, transform.position)) {
				closestEnemy = mb.transform;
			}
		}
	}

	void Die() {
		GameObject.Destroy(gameObject);
	}

	protected virtual void Charge(Transform target) {
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

		if(Vector2.Angle(transform.up * -1, targetDelta) < 25) {
			eyes.sprite = charge;
			int MOV = STR;
			if (Vector2.Distance(target.position, transform.position) < CHARGE_DISTANCE)
				MOV *= 2;
			GetComponent<Rigidbody2D>().AddForce(targetDelta.normalized * MOV);
		}
	}

	public Vector2 turnRotation, moveDirection;
	Vector2 targetDelta;
	Vector2 averageAlignment;
	void Normal() {
		eyes.sprite = normal;
		
		Vector2 averagePosition = transform.position;
		
		
		Transform closestAlly = null;
		if (update <= 0) {
			averageAlignment = transform.up * -1;
			allies.RemoveAll(s => s == null);
			foreach (MarblePawn mb in allies) {
				averageAlignment += (Vector2)mb.transform.up * -1;
				averagePosition += (Vector2)mb.transform.position;

				if (!closestAlly || Vector2.Distance(closestAlly.position, transform.position) > Vector2.Distance(mb.transform.position, transform.position)) {
					closestAlly = mb.transform;
				}
			}
			averagePosition /= allies.Count + 1;
			//Cohesion
			targetDelta = averagePosition - (Vector2)transform.position;
			//Separation
			if (closestAlly && Vector2.Distance(closestAlly.position, transform.position) < SEPARATE_DISTANCE) {
				targetDelta = targetDelta.normalized + (Vector2)(transform.position - closestAlly.position).normalized;
			}
			update = Random.value + .2f;
			//Forward Direction
			targetDelta = targetDelta.normalized + (Vector2)(-3f * transform.up);
		} else update -= Time.deltaTime;

		

		if (moveDirection == null || moveDirection.magnitude == 0) { moveDirection = targetDelta.normalized; }

		GetComponent<Rigidbody2D>().AddForce(moveDirection.normalized * STR);
		moveDirection = Vector2.zero;

		averageAlignment.Normalize();
		if (turnRotation == null || turnRotation.magnitude == 0) 
			{ turnRotation = averageAlignment.normalized * 2 + targetDelta.normalized; } 
		//Alignment
		float angleDiff = Vector2.SignedAngle(transform.up * -1, turnRotation);
		float turn = angleDiff;
		if (angleDiff > 1)
			turn = 1;
		else if (angleDiff < -1)
			turn = -1;
		// apply torque along that axis according to the magnitude of the angle.
		GetComponent<Rigidbody2D>().AddTorque(turn * TURN);
		turnRotation = Vector2.zero;
	}

	void Stun() {
		eyes.sprite = stunned;
	}
	public void Damage(int DMG, Vector2 incoming) { 
		if (DMG == 0)
			return;
		Vector3 facing = transform.up * -1;
		Vector3 toTarget = incoming * -1;
		float dot = Vector3.Dot(facing, toTarget);
		//DebugLog("Facing " + dot);
		float cosFovAngle = Mathf.Cos((45 / 2) * Mathf.Deg2Rad);
		if (dot > cosFovAngle)// the positin is within our fov angle 
		{
			DMG = Mathf.Max(0, DMG - SHLD);
		}
		HP -= DMG;
		if (DMG > 1)
			STUN = 1;
		//DebugLog("KE " + KineticEnergy(attacker.marble));
		//DebugLog("Dealt Damage:" + (int)( attacker.ATK * KineticEnergy(attacker.marble) / DEF));
		if (HP <= 0) {
			Die();
		}
	}

	public void Damage(MarblePawn attacker) {
		Damage((int)Mathf.CeilToInt(attacker.ATK * (1 + (KineticEnergy(attacker.marble) / MASS)) / DEF), (transform.position - attacker.transform.position).normalized);
	}
	public static float KineticEnergy(Rigidbody2D rb) {
		// mass in kg, velocity in meters per second, result is joules
		return 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
	}
	public List<MarblePawn> enemies;
	public List<MarblePawn> allies;

	private void OnTriggerEnter2D(Collider2D collision) {
		MarblePawn otherMB = collision.gameObject.GetComponentInChildren<MarblePawn>();
		if (otherMB) {
			if (key != otherMB.key) {
				if(!enemies.Contains(otherMB))
					enemies.Add(otherMB);
			} else {
				if (!allies.Contains(otherMB))
					allies.Add(otherMB);
			}
		}
	}
	private void OnTriggerExit2D(Collider2D collision) {
		MarblePawn otherMB = collision.gameObject.GetComponentInChildren<MarblePawn>();
		if (otherMB) {
			if (key != otherMB.key) {
				enemies.Remove(otherMB);
			} else {
				allies.Remove(otherMB);
			}
		}
	}
	float attack = 1;
	private void OnCollisionStay2D(Collision2D collision) {
		if (attack > 0) {
			attack -= Time.deltaTime;
		} else {
			MarblePawn otherMB = collision.gameObject.GetComponentInChildren<MarblePawn>();
			if (otherMB) {
				if (key != otherMB.key) {
					//Attack
					attack = 1;
					Vector3 facing = transform.up * -1;
					Vector3 toTarget = (otherMB.transform.position - transform.position).normalized;
					float dot = Vector3.Dot(facing, toTarget);
					//DebugLog("Facing " + dot);
					float cosFovAngle = Mathf.Cos((45 / 2) * Mathf.Deg2Rad);
					if (dot > cosFovAngle)// the positin is within our fov angle 
					{
						//DebugLog("Facing Corret");
						otherMB.Damage(this);
						otherMB.marble.AddForce(transform.up * -1 * STR * 10);
						marble.AddForce(transform.up * STR * 10);
					}
				}
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D collision) {
		MarblePawn otherMB = collision.gameObject.GetComponentInChildren<MarblePawn>();
		if (otherMB) {
			//DebugLog("Detected collision with Marble");
			if (key != otherMB.key) {
				//DebugLog("Enemy Marlbe");
				//Attack
				attack = 1;
				Vector3 facing = transform.up * -1;
				Vector3 toTarget = (otherMB.transform.position - transform.position).normalized;
				float dot = Vector3.Dot(facing, toTarget);
				//DebugLog("Facing " + dot);
				float cosFovAngle = Mathf.Cos((45 / 2) * Mathf.Deg2Rad);
				if (dot > cosFovAngle)// the positin is within our fov angle 
				{
					//DebugLog("Facing Corret");
					otherMB.Damage(this);
				}
			}
		}
	}

}
