using UnityEngine;
using System.Collections;

public class MoveCaptain : MonoBehaviour {

	public float captainSpeed;

	private GameObject laserButton;
	private GameObject shieldButton;
	private GameObject engineButton;
	private GameObject[] buttons;

	private float moveX;
	private float moveY;

	private bool moving;

	private int laserShieldEngineNone;


	// Use this for initialization
	void Start () {
		buttons = new GameObject[3];
		buttons[0] = laserButton = GameObject.FindGameObjectWithTag ("Laser").transform.GetChild (0).gameObject;
		buttons[1] = shieldButton = GameObject.FindGameObjectWithTag ("Shield").transform.GetChild (0).gameObject;
		buttons[2] = engineButton = GameObject.FindGameObjectWithTag ("Engine").transform.GetChild (0).gameObject;
		moving = false;
		moveX = 0f;
		moveY = 0f;
		laserShieldEngineNone = 3;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (moving) {
			float diffX = gameObject.transform.position.x - buttons [laserShieldEngineNone].transform.position.x;
			float diffY = gameObject.transform.position.y-buttons[laserShieldEngineNone].transform.position.y;
			if (Mathf.Abs(diffX) < captainSpeed  &&  Mathf.Abs(diffY) < captainSpeed) {
				gameObject.transform.Translate(-diffX, -diffY, 0);
				moving = false;
			} else { 
				gameObject.transform.Translate (moveX, moveY, 0);
			}
		}
	}

	public bool GoToLaserShieldEngine(string dest) {
		if (moving) {
			return false;
		}
		moving = true;
		GameObject button = null;
		char first = dest [0];
		if (first == 'L') {
			laserShieldEngineNone = 0;
			button = laserButton;
		} else if (first == 'S') {
			laserShieldEngineNone = 1;
			button = shieldButton;
		} else if (first == 'E') {
			laserShieldEngineNone = 2;
			button = engineButton;
		}
		Vector3 direction = button.transform.position - gameObject.transform.position;
		moveX = (direction.x * captainSpeed) /	direction.magnitude;
		moveY = (direction.y * captainSpeed) / direction.magnitude;
		return true;
	}
}
