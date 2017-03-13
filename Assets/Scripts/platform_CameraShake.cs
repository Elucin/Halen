using UnityEngine;
using System.Collections;

public class platform_CameraShake : MonoBehaviour {

	private float duration;
	private float magnitude;

	//public bool test = false;

	// -------------------------------------------------------------------------
	public void PlayShake() {

		StopAllCoroutines();
		StartCoroutine("Shake");
	}

	public void StopShake()
	{
		StopAllCoroutines ();
	}

	// -------------------------------------------------------------------------
	void Start() {
		duration = 20.0f;
		magnitude = 0.15f;
	}

	// -------------------------------------------------------------------------
	IEnumerator Shake() {
		float elapsed = 0.0f;

		while (elapsed < duration) {
			

			elapsed += Time.deltaTime;			

			float percentComplete = elapsed / 20.0f;			
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map noise to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			Camera.main.transform.position = new Vector3(x + Camera.main.transform.position.x, y + Camera.main.transform.position.y, Camera.main.transform.position.z);

			yield return null;
		}
	}
}




