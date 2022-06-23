using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GyroScript : MonoBehaviour {

	public Material material1;
	public Material material2;
	private bool materialToggle;
	private float movementSpeed = 0.05f;
	private float rotationSpeed = 0.2f;
	
	
	private Vector3 vorne;
	private Vector3 hinten;
	private Vector3 links;
	private Vector3 rechts;
	private Vector3 unten;


	public GameObject playerCube;
	public CharacterController controller;

	private Rigidbody rb;

	//Working on Gravity
	Vector3 velocity;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
	bool cameraIsActive = false;
	void Awake () {
		AirConsole.instance.onMessage += OnMessage;
	}

	void OnMessage (int from, JToken data){
		
		//Debug.Log ("message from device " + from + ", data: " + data); 
		//falling down

		switch (data ["action"].ToString ()) {
		case "motion":
        			

			if (data ["motion_data"] != null) {

				if (data ["motion_data"] ["x"].ToString() != "") {

					Vector3 abgAngles = new Vector3 (-(float)data ["motion_data"] ["beta"], -(float)data ["motion_data"] ["alpha"], -(float)data ["motion_data"] ["gamma"]);
					Debug.Log ("abgAngles.x: " + abgAngles.x + "abgAngles.y: " + abgAngles.y + "abgAngles.z: " + abgAngles.z);
//Movement			
					isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
					if (isGrounded && velocity.y < 0)
        			{
            			velocity.y = -2f;
        			}

					float x = Input.GetAxis("Horizontal");
        			float z = Input.GetAxis("Vertical");
					var rigidbody = GetComponent < Rigidbody > ();
					if(cameraIsActive == false){
						rigidbody.velocity = new Vector3(abgAngles.y * 0.5f, rigidbody.velocity.y, abgAngles.x * 0.5f);
					} else {
						if (abgAngles.y < -10) {
							playerCube.transform.Rotate(new Vector3(0,abgAngles.y * rotationSpeed,0), Space.Self);
						}
						if (abgAngles.y > 10){
							playerCube.transform.Rotate(new Vector3(0,abgAngles.y * rotationSpeed,0), Space.Self);
						}
						
							
					}
				/*		
					if (abgAngles.x > 10) {
						vorne = new Vector3 (0, 0, 1);
						playerCube.transform.Translate (vorne * movementSpeed*abgAngles.x);
					}
					if (abgAngles.x < -10) {
						hinten = new Vector3 (0, 0, -1);
						playerCube.transform.Translate (hinten * movementSpeed*(-abgAngles.x));
					}
					if (abgAngles.y > 10) {
						rechts = new Vector3 (1, 0, 0);
						playerCube.transform.Translate (rechts * movementSpeed*abgAngles.y);
					}
					if (abgAngles.y < -10) {
						links = new Vector3 (-1, 0, 0);
						playerCube.transform.Translate (links * movementSpeed*-abgAngles.y);
					}
					} else {
						if (abgAngles.y < -10) {
							playerCube.transform.Rotate(new Vector3(0,abgAngles.y * rotationSpeed,0), Space.Self);
						}
						if (abgAngles.y > 10){
							playerCube.transform.Rotate(new Vector3(0,abgAngles.y * rotationSpeed,0), Space.Self);
						}
						
							
					}

					velocity.y += gravity * Time.deltaTime;
        			controller.Move(velocity * Time.deltaTime);
*/

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
		case "active-camera":
			if (cameraIsActive == true){
				cameraIsActive = false;
			} else {
				cameraIsActive = true;
			}
			//playerCube.transform.Rotate(new Vector3(0,-(float)data ["motion_data"] ["alpha"] * rotationSpeed,0), Space.Self);
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
