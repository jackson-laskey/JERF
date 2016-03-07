using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {
	public GameController controller;


	// Use this for initialization
	void Start () {
		this.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		controller.init (true);
		this.gameObject.SetActive (false);
	}
}
