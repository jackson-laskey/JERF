using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// how many units the projectile moves per second; negative if the player is firing
	public float speed;
	// dictate the top and bottom of the view
	public float maxY;
	public float minY;

	public void init(bool isEnemy) {
		if (!isEnemy) {
			speed = -speed;
		}
	}
	
	// if out of view, 
	protected void Update () {
		if (transform.position.y > maxY || transform.position.y < minY) {
			Destroy (gameObject);
		}
		Move ();
	}

	protected void Move() {
		transform.position = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime*speed));
	}

	public void Hit() {
		Destroy(gameObject);
	}
}
