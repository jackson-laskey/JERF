using UnityEngine;
using System.Collections;

public class MoveCaptain : MonoBehaviour {

	// units per second of movement
	public float captainSpeed;

	// the rendered objects of the different buttons
	private GameObject laserButton;
	private GameObject shieldButton;
	private GameObject engineButton;
	// holds the above objects
	private GameObject[] buttons;

	// filled with the directions and distance to move in the x and y per second
	private float moveX;
	private float moveY;

	// whether captain is on the move
	private bool moving;

	// what component is being repaired. 0=laser, 1=shield, 2=engine, 3=none
	private int laserShieldEngineNone;


	// builds the "buttons", initializes other fields to non-moving state
	void Start () {
		// building buttons array and finding its elements
		buttons = new GameObject[3];
		buttons[0] = laserButton = GameObject.Find ("Lasers").transform.GetChild (0).gameObject;
		buttons[1] = shieldButton = GameObject.Find ("Shields").transform.GetChild (0).gameObject;
		buttons[2] = engineButton = GameObject.Find ("Engines").transform.GetChild (0).gameObject;

		// nothing is activated, captain is not moving
		laserShieldEngineNone = 3;
		moving = false;
		moveX = 0f;
		moveY = 0f;

		captainSpeed = 1;
	}

	void Update () {
		if (moving) {
			float diffX = gameObject.transform.position.x - buttons [laserShieldEngineNone].transform.position.x;
			float diffY = gameObject.transform.position.y-buttons[laserShieldEngineNone].transform.position.y;
			if (Mathf.Abs(diffX) < captainSpeed*Time.deltaTime*2  &&  Mathf.Abs(diffY) < captainSpeed*Time.deltaTime*2) {
				gameObject.transform.Translate(-diffX, -diffY, 0);
				moving = false;
			} else { 
				gameObject.transform.Translate (moveX*Time.deltaTime, moveY*Time.deltaTime, 0);
			}
		}
	}

	public bool GoToLaserShieldEngine(string dest) {
		// keeps current destination if captain is already in motion
		if (moving) {
			return false;
		}
	
		moving = true;
		// button will hold a reference to the destination button
		GameObject button = null;
		// takes the first character of the tag of the component calling the function
		char first = dest [0];
		// Lasers
		if (first == 'L') {
			laserShieldEngineNone = 0;
			button = laserButton;
		} 
		// Shields
		else if (first == 'S') {
			laserShieldEngineNone = 1;
			button = shieldButton;
		} 
		// Engines
		else if (first == 'E') {
			laserShieldEngineNone = 2;
			button = engineButton;
		}
		// sets the moveX and moveY to correspond to the path from the current button to the destination button
		Vector3 direction = button.transform.position - gameObject.transform.position;
		moveX = (direction.x * captainSpeed);
		moveY = (direction.y * captainSpeed);
		return true;
	}
}
