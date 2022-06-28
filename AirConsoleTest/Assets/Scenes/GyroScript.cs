using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GyroScript : MonoBehaviour {

	public Material material1;
	public Material material2;
	private bool materialToggle;
	public float movementSpeed;
	public float rotationSpeed;
	
	
	private Vector3 vorne;
	private Vector3 hinten;
	private Vector3 links;
	private Vector3 rechts;
	private Vector3 unten;

	//jumping
	public float jumpForce;


	public GameObject playerCube;
	public CharacterController controller;

	Vector3 abgAngles; 

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
		//add a rigidbody if it doesn't exist yet
		if (rb == null) {
			rb = playerCube.GetComponent<Rigidbody> ();
		}

		//Debug.Log ("message from device " + from + ", data: " + data); 
		//falling down
		switch (data ["action"].ToString ()) {
		case "motion":
        			
			if (data ["motion_data"] != null) {

				if (data ["motion_data"] ["x"].ToString() != "") {

					abgAngles = new Vector3 (-(float)data ["motion_data"] ["beta"], -(float)data ["motion_data"] ["alpha"], -(float)data ["motion_data"] ["gamma"]);
					Debug.Log ("abgAngles.x: " + abgAngles.x + "abgAngles.y: " + abgAngles.y + "abgAngles.z: " + abgAngles.z);

					//velocity.y += gravity * Time.deltaTime;
        			//controller.Move(velocity * Time.deltaTime);
				}
			}

			break;
		case "shake":
			//rb.velocity = rb.velocity + new Vector3 (0, jumpForce, 0);
			if (isGrounded) {
				//rb.AddForce (new Vector3 (0, jumpForce, 0), ForceMode.Impulse);
				rb.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
			}else{

			}
			
			//i love it 
			//rb.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);

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

	void Update(){

		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
	
		//Movement								
			if (cameraIsActive == false) {	
				if (abgAngles.x > 10) {
					vorne = new Vector3 (0, 0, 1);
					playerCube.transform.Translate (vorne * movementSpeed*abgAngles.x* Time.deltaTime);
				}
				if (abgAngles.x < -10) {
					hinten = new Vector3 (0, 0, -1);
					playerCube.transform.Translate (hinten * movementSpeed*(-abgAngles.x)* Time.deltaTime);
				}
				if (abgAngles.y > 10) {
					rechts = new Vector3 (1, 0, 0);
					playerCube.transform.Translate (rechts * movementSpeed*abgAngles.y* Time.deltaTime);
				}
				if (abgAngles.y < -10) {
					links = new Vector3 (-1, 0, 0);
					playerCube.transform.Translate (links * movementSpeed*-abgAngles.y* Time.deltaTime);
				}
			} else {
				if (abgAngles.y < -10) {
					playerCube.transform.Rotate(new Vector3(0,abgAngles.y * rotationSpeed * Time.deltaTime ,0), Space.Self);
				}
				if (abgAngles.y > 10){
					playerCube.transform.Rotate(new Vector3(0,abgAngles.y * rotationSpeed * Time.deltaTime ,0), Space.Self);
				}								
			}
			
	}

	void OnDestroy () {
		if (AirConsole.instance != null) {
			AirConsole.instance.onMessage -= OnMessage;		
		}
	}
}
