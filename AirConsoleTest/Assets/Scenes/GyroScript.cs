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
	public GameObject treasure;
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
	// create variable airconsole
	public string treasureDistance;
	public float distanceTreasurePlayer;

	//working on Proximity
	public Proximity proximity;

	void Awake () {
		AirConsole.instance.onMessage += OnMessage;
		//access variable from gyroscope-controller.html
		//find by gametag "treasure"
		treasure = GameObject.FindGameObjectWithTag("Treasure");
		InvokeRepeating ("vibrateMethode", 3.3f, 3.3f);
	
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
					//Debug.Log ("abgAngles.x: " + abgAngles.x + "abgAngles.y: " + abgAngles.y + "abgAngles.z: " + abgAngles.z);

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
		case "vibrate":
		var message = new {
    action = "move"
};
			AirConsole.instance.Message(1, message);
			break;
		case "message":
			Debug.Log("messagegegee");
			break;
		case "message2":
			Debug.Log("adadadad");
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
			calculateDistance();




	}

	void OnDestroy () {
		if (AirConsole.instance != null) {
			AirConsole.instance.onMessage -= OnMessage;		
		}
	}

	void vibrateMethode(){
		if(proximity.zustand == 1){
		var message = new {
    	action = "vibrate_cold"
		};
		AirConsole.instance.Message(1, message);
		Debug.Log(message);
		};
		if (proximity.zustand == 2){
			var message = new {
			action = "vibrate_warm"
		};
		AirConsole.instance.Message(1, message);
		Debug.Log(message);
		}
		if (proximity.zustand == 3){
			var message = new {
			action = "vibrate_hot"
		};
				AirConsole.instance.Message(1, message);
				Debug.Log(message);
		}
		Debug.Log(treasureDistance);
		Debug.Log(distanceTreasurePlayer);
	}

	// function to calculate the distance
	void calculateDistance(){

		distanceTreasurePlayer = Vector3.Distance (playerCube.transform.position, treasure.transform.position);
		if (distanceTreasurePlayer <= 8) {
			treasureDistance = "hot";
		} 
		else if (distanceTreasurePlayer <= 16 && distanceTreasurePlayer > 8) {
			treasureDistance = "warm";
		}
		else if (distanceTreasurePlayer > 16 && distanceTreasurePlayer <= 24) {
			treasureDistance = "cold";
		}
		else if (distanceTreasurePlayer > 24) {
			treasureDistance = "nothing";
		}

	}
}