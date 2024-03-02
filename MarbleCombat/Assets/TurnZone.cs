using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnZone : MonoBehaviour
{
	public int key;

	private void OnTriggerStay2D(Collider2D collision) {
		MarblePawn otherMB = collision.gameObject.GetComponentInChildren<MarblePawn>();
		if (otherMB) {
			if (key == otherMB.key || key < 0) {
				otherMB.turnRotation = transform.right;
				//otherMB.moveDirection = transform.right;
			}
		}
	}
}
