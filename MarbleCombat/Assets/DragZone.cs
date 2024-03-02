using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragZone : MonoBehaviour
{
	public int drag;
	private void OnTriggerEnter2D(Collider2D collision) {
		collision.attachedRigidbody.drag += drag;
	}

	private void OnTriggerExit2D(Collider2D collision) {
		collision.attachedRigidbody.drag -= drag;
	}
}
