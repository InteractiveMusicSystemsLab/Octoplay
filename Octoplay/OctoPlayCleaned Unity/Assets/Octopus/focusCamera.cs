using UnityEngine;
using System.Collections;

public class focusCamera : MonoBehaviour {

	static GameObject mainCamera;
	
	
	// Use this for initialization
	void Start ()
	{
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	public static IEnumerator switchCameras(Transform cameraTarget)
	{
		float t = 0.0f;
		Vector3 startingPos = mainCamera.transform.position;
		Quaternion startingRot = mainCamera.transform.rotation;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale/1.0f);
			
			mainCamera.transform.position = Vector3.Lerp(startingPos, cameraTarget.position, t);
			mainCamera.transform.rotation = Quaternion.Lerp(startingRot, Quaternion.Euler(new Vector3(cameraTarget.transform.rotation.eulerAngles.x, cameraTarget.transform.rotation.eulerAngles.y, cameraTarget.transform.rotation.eulerAngles.z)), t);
			
			yield return null;
		}
	}
}
