using UnityEngine;
using System.Collections;

public class MoveCaptain : MonoBehaviour {

	public GameController controller;

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
	public void init (GameController gContr, GameObject[] components) {
		controller = gContr;

		ButtonClicker buttonClickerComponent;
		ComponentHealth healthComponent;
		GameObject button;

		// laser health objects
		// health bar- .9x, .052y, .6x, .125y
		laserButton = new GameObject();
		laserButton.name = "Health";
		laserButton.transform.parent = components [0].transform;
		laserButton.transform.localPosition = new Vector3 (.9f, .03f, 0);
		laserButton.transform.localScale = new Vector3(.6f, 1.25f, 1);
		healthComponent = laserButton.AddComponent<ComponentHealth> ();
		healthComponent.init (controller, 0, 0);
		// button
		button = new GameObject ();
		button.name = "Button";
		button.transform.parent = components[0].transform;
		button.transform.localPosition = new Vector3 (0, 0, 0);
		buttonClickerComponent = button.AddComponent<ButtonClicker> ();
		buttonClickerComponent.init (controller, 0, 1, 1, 0, 0);

		// shield health objects
		// health bar
		shieldButton = new GameObject();
		shieldButton.name = "Health";
		shieldButton.transform.parent = components [1].transform;
		shieldButton.transform.localPosition = new Vector3 (-.53f, -3.48f, 0);
		shieldButton.transform.localScale = new Vector3(.6f, 1.25f, 1);
		healthComponent = shieldButton.AddComponent<ComponentHealth> ();
		healthComponent.init (controller, 0, 0);
		//button
		button = new GameObject ();
		button.name = "Button";
		button.transform.parent = components[1].transform;
		button.transform.localPosition = new Vector3 (.15f, -.3f, 0);
		buttonClickerComponent = button.AddComponent<ButtonClicker> ();
		buttonClickerComponent.init (controller, .85f, -1.6f, 1, .5f, 0);

		// engine health objects
		// health bar
		engineButton = new GameObject();
		engineButton.name = "Health";
		engineButton.transform.parent = components [2].transform;
		engineButton.transform.localPosition = new Vector3 (.5f, -3.48f, 0);
		engineButton.transform.localScale = new Vector3(.6f, 1.25f, 1);
		healthComponent = engineButton.AddComponent<ComponentHealth> ();
		healthComponent.init (controller, 0, 0);
		// button
		button = new GameObject ();
		button.name = "Button";
		button.transform.parent = components[2].transform;
		button.transform.localPosition = new Vector3 (-.15f, -.3f, 0);
		buttonClickerComponent = button.AddComponent<ButtonClicker> ();
		buttonClickerComponent.init (controller, -.85f, -1.6f, 0, 0, 1);

		buttons = new GameObject[3]{laserButton, shieldButton, engineButton};

		// nothing is activated, captain is not moving
		laserShieldEngineNone = 3;
		moving = false;
		moveX = 0f;
		moveY = 0f;

		captainSpeed = 1;

		GoToLaserShieldEngine ("Shields");
	}

	void Update () {
		if (moving) {
			float diffX = gameObject.transform.position.x - buttons [laserShieldEngineNone].transform.position.x;
			float diffY = gameObject.transform.position.y-buttons[laserShieldEngineNone].transform.position.y;
			if (Mathf.Abs(diffX) < captainSpeed*Time.deltaTime*4  &&  Mathf.Abs(diffY) < captainSpeed*Time.deltaTime*4) {
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
