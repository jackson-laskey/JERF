using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// how many units the projectile moves per second; negative if the player is firing
	protected float speed;
	// dictate the top and bottom of the view
	private float maxY;
	private float minY;

	protected GameObject model;

	protected void init(bool isEnemy, string textureName, float xScale, float yScale, int textPixels) {
		minY = -5;
		maxY = 5;
		if (!isEnemy) {
			speed = -speed;
		}
		GameObject.Find ("GameController").GetComponent<GameController> ().MakeSprite (gameObject, textureName, transform.parent,
			transform.position.x, transform.position.y, xScale, yScale, textPixels);
		gameObject.AddComponent<BoxCollider2D> ().isTrigger = true;
		gameObject.GetComponent<BoxCollider2D> ().size = new Vector2 (.1f, .1f);
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
