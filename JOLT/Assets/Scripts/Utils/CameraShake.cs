using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class CameraShake : Singleton<CameraShake> {
	[Header("Shake properties")]
	[SerializeField, Tooltip("How long should the shake last for?")] private float shakeDuration;
	[SerializeField, Tooltip("How strong is the shake?")] private float shakeAmount;

	private Vector3 currentPos;

	public void TriggerShake() {
		StartCoroutine(Shake());
	}

	private IEnumerator Shake() {
		// Initialize a timer.
		float currentShakeDuration = shakeDuration;

		// Save the position before shaking.
		currentPos = transform.localPosition;

		while(currentShakeDuration > 0) {
			// Randomly move the camera around.
			transform.localPosition = currentPos + Random.insideUnitSphere * shakeAmount;

			// Start counting down before stopping the shake.
			currentShakeDuration -= Time.unscaledDeltaTime;

			yield return new WaitForEndOfFrame();
		}

		// Move the camera back to its previous position.
		transform.localPosition = currentPos;
	}
}
