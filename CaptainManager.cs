using UnityEngine;
using System.Collections;

public class CaptainManager : MonoBehaviour {

	public GameController controller;

	public GameObject lasers;
	public GameObject engines;
	public GameObject shields;
	public GameObject[] components;

	public GameObject crew;


	// Use this for initialization
	public void init (GameController gContr) {
		controller = gContr;
		transform.localScale = new Vector3(1.5f, 1.5f, 1);
		transform.position = new Vector3(3.5f, 0, 0);

		lasers = new GameObject();
		lasers.transform.parent = transform;
		lasers.transform.localPosition = new Vector3 (0, 0, 0);
		lasers.name = "Lasers";
		engines = new GameObject();
		engines.transform.parent = transform;
		engines.transform.localPosition = new Vector3 (0, 0, 0);
		engines.name = "Engines";
		shields = new GameObject();
		shields.transform.parent = transform;
		shields.transform.localPosition = new Vector3 (0, 0, 0);
		shields.name = "Shields";
		components = new GameObject[3]{ lasers, engines, shields };

		crew = new GameObject ();
		crew.transform.parent = transform;
		crew.name = "Crew";
		crew.AddComponent<MoveCaptain> ();
		crew.GetComponent<MoveCaptain> ().init (controller, components);
	}
}
