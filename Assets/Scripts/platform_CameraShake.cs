using UnityEngine;
using System.Collections;

public class platform_CameraShake : MonoBehaviour {

	public float duration;
	public float magnitude;
	public Camera camera;

	//private Transform OriginTrans;

	void Awake() {
		PlayShake ();

		//OriginTrans.position = camera.transform.position;
	}

	// -------------------------------------------------------------------------
	public void PlayShake() {

		StartCoroutine("Shake");
	}

	// -------------------------------------------------------------------------


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

			camera.transform.position = new Vector3(camera.transform.position.x, y + camera.transform.position.y, x + camera.transform.position.z);


			yield return null;

		}
	}
}