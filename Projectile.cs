using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed;
	public float maxY;


	// Use this for initialization
	//public void init(float moveSpeed) {
	//	speed = moveSpeed;
	//}

	protected void Start() {
		print ("here");
	}
	
	// Update is called once per frame
	protected void Update () {
		if (gameObject.transform.position.y > maxY) {
			Destroy (gameObject);
		}
		Move ();
	}

	protected void Move() {
		gameObject.transform.Translate (0, speed, 0);
	}
}
