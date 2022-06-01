using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GyroscopeExampleLogic : MonoBehaviour {

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

	void Awake () {
		AirConsole.instance.onMessage += OnMessage;		
	}

	void OnMessage (int from, JToken data){
		
		//Debug.Log ("message from device " + from + ", data: " + data); 

		switch (data ["action"].ToString ()) {
		case "motion":
			

			if (data ["motion_data"] != null) {

				if (data ["motion_data"] ["x"].ToString() != "") {

//test begin
					//float translation = "motion data"(vertical) * movementSpeed;
					//float rotation = "motion data" (horizontal) * rotationSpeed;
					//translation *= Time.deltaTime;
					//rotation *= Time.deltaTime;
					//transform.Translate (0, 0, translation);
					//transform.Rotate (0, rotation, 0);

//test end
					//Vector3 abgAngles = new Vector3 (-(float)data ["motion_data"] ["beta"], -(float)data ["motion_data"] ["alpha"], -(float)data ["motion_data"] ["gamma"]);
					//Abfrage ob Schwellenwert für rechts, links, vorne, hinten errreicht ist.
					if(-(float)data ["motion_data"] ["beta"] > -80 && -(float)data ["motion_data"] ["beta"] < 0){
						vorne = new Vector3 (0, 0, movementSpeed);
						playerCube.transform.Translate (vorne);
					}

					if(-(float)data ["motion_data"] ["beta"] < -100 && -(float)data ["motion_data"] ["beta"] > -180){
						hinten = new Vector3 (0, 0, -movementSpeed);
						playerCube.transform.Translate (hinten);
					}

					if(-(float)data ["motion_data"] ["gamma"] > 5 && -(float)data ["motion_data"] ["gamma"] < 80){
						links = new Vector3 (-movementSpeed, 0, 0);
						playerCube.transform.Translate (links);
					}
					if(-(float)data ["motion_data"] ["gamma"] < -5 && -(float)data ["motion_data"] ["gamma"] > -80){
						rechts = new Vector3 (movementSpeed, 0, 0);
						playerCube.transform.Translate (rechts);
					}

					Debug.Log("motion data alpha" + -(float)data ["motion_data"] ["alpha"] + " beta" + -(float)data ["motion_data"] ["beta"] + " gamma: " + -(float)data ["motion_data"] ["gamma"] + "Time.deltaTime " + Time.deltaTime);
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
