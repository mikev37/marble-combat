﻿using UnityEngine;

public class CameraDrag : MonoBehaviour
{
	private Vector3 ResetCamera;
	private Vector3 Origin;
	private Vector3 Diference;
	private bool Drag = false;
	void Start() {
		QualitySettings.vSyncCount = 1;

		Application.targetFrameRate = 60;
		ResetCamera = Camera.main.transform.position;
	}
	void LateUpdate() {
		if (Input.GetMouseButton(0)) {
			Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
			if (Drag == false) {
				Drag = true;
				Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
		} else {
			Drag = false;
		}
		if (Drag == true) {
			Camera.main.transform.position = Origin - Diference;
		}
		//RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
		if (Input.GetMouseButton(1)) {
			Camera.main.transform.position = ResetCamera;
		}
	}
}