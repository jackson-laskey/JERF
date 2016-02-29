using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// how many units the projectile moves per second; negative if the player is firing
	protected float speed;
	// dictate the top and bottom of the view
	private float maxY;
	private float minY;

	protected Color color;
	protected GameObject model;

	protected void init(bool isEnemy) {
		minY = -5;
		maxY = 5;
		if (!isEnemy) {
			speed = -speed;
		}
		MakeModel ();
		Collider2D collider = gameObject.AddComponent<BoxCollider2D> ();
		collider.isTrigger = true;
	}

	protected void MakeModel() {
		model = GameObject.CreatePrimitive (PrimitiveType.Quad);
		model.transform.parent = gameObject.transform;
		model.transform.localPosition = new Vector3 (0, 0, 0);
		model.transform.localScale = transform.localScale;
		model.name = gameObject.name + "Model";
		Material mat = model.GetComponent<Renderer> ().material;
		mat.shader = Shader.Find ("Sprites/Default");
		mat.mainTexture = Resources.Load<Texture2D> ("Textures/PlayerLaser");
		mat.color = color;
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
