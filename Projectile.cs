using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// how many units the projectile moves per second; negative if the player is firing
	protected float speed;
	// dictate the top and bottom of the view
	private float maxY = 5;
	private float minY = -5;
	protected float colliderSizex = .1f;
	protected float colliderSizey = .6f;

	protected GameObject model;

	protected void init(bool isEnemy, string textureName, float xScale, float yScale, int textPixels) {

		if (!isEnemy) {
			speed = -speed;
		}
		GameObject.Find ("GameController").GetComponent<GameController> ().MakeSprite (gameObject, textureName, transform.parent,
				transform.position.x, transform.position.y, xScale, yScale, textPixels);
		gameObject.AddComponent<BoxCollider2D> ().isTrigger = true;
		if (textureName == "SuperPlayerLaser") {
			colliderSizex = .1f;
			colliderSizey = .6f;
			gameObject.GetComponent<BoxCollider2D> ().size = new Vector2 (colliderSizex, colliderSizey);
			gameObject.GetComponent<BoxCollider2D> ().offset = new Vector2 (0, -.3f);
		}
		if (textureName == "PlayerLaser") {
			colliderSizex = .1f;
			colliderSizey = .4f;
			gameObject.GetComponent<BoxCollider2D> ().size = new Vector2 (colliderSizex, colliderSizey);
			gameObject.GetComponent<BoxCollider2D> ().offset = new Vector2 (0, -.2f);
		}
		if (textureName == "Laser") {
			colliderSizex = .1f;
			colliderSizey = .5f;
			gameObject.GetComponent<BoxCollider2D> ().size = new Vector2 (colliderSizex, colliderSizey);
			gameObject.GetComponent<BoxCollider2D> ().offset = new Vector2 (0, -.25f);
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
		Destroy (gameObject);
	}

}
