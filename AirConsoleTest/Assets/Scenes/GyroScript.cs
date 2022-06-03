using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GyroScript : MonoBehaviour {

	public Material material1;
	public Material material2;
	private bool materialToggle;
	private float movementSpeed = 0.5f;
	private float rotationSpeed = 10F;
	
	
	private Vector3 vorne;
	private Vector3 hinten;
	private Vector3 links;
	private Vector3 rechts;


	public GameObject playerCube;

	private Rigidbody rb;

	void Awake () {
		AirConsole.instance.onMessage += OnMessage;		
	}

	void OnMessage (int from, JToken data){
		
		//Debug.Log ("message from device " + from + ", data: " + data); 

		switch (data ["action"].ToString ()) {
		case "motion":
			

			if (data ["motion_data"] != null) {

				if (data ["motion_data"] ["x"].ToString() != "") {

					Vector3 abgAngles = new Vector3 (-(float)data ["motion_data"] ["beta"], -(float)data ["motion_data"] ["alpha"], -(float)data ["motion_data"] ["gamma"]);
					Debug.Log ("abgAngles.x: " + abgAngles.x + "abgAngles.y: " + abgAngles.y + "abgAngles.z: " + abgAngles.z);
//Movement
					if (abgAngles.x > 10) {
						vorne = new Vector3 (0, 0, 1);
						playerCube.transform.Translate (vorne * movementSpeed);
					}
					if (abgAngles.x < -10) {
						hinten = new Vector3 (0, 0, -1);
						playerCube.transform.Translate (hinten * movementSpeed);
					}
					if (abgAngles.y > 10) {
						rechts = new Vector3 (1, 0, 0);
						playerCube.transform.Translate (rechts * movementSpeed);
					}
					if (abgAngles.y < -10) {
						links = new Vector3 (-1, 0, 0);
						playerCube.transform.Translate (links * movementSpeed);
					}




//Working rotation
/*					playerCube.transform.eulerAngles = abgAngles;
					Debug.Log ("abgAngles " + abgAngles);


					Debug.Log ("myRotation " + playerCube.transform.eulerAngles);
*/					//Debug.Log("motion data alpha" + -(float)data ["motion_data"] ["alpha"] + " beta" + -(float)data ["motion_data"] ["beta"] + " gamma: " + -(float)data ["motion_data"] ["gamma"] + "Time.deltaTime " + Time.deltaTime);
//end of block comment
				}
			}

			break;
		case "shake":
			//the cube changes color on shake
			if (materialToggle) {
				playerCube.GetComponent<Renderer> ().materials = new Material[]{ material1 };
				materialToggle = false;
			} else {
				playerCube.GetComponent<Renderer> ().materials = new Material[]{ material2 };
				materialToggle = true;
			}
			break;
		default:
			Debug.Log (data);
			break;
		}
	}

	void OnDestroy () {
		if (AirConsole.instance != null) {
			AirConsole.instance.onMessage -= OnMessage;		
		}
	}
}
