using UnityEngine;
using System.Collections;

public class CannonEnemy : ParentEnemy {

	private CannonEnemyModel model;
	private float stopPosition;
	private string direction;
	private string firingSide;

	private float cd;

	public void init(EnemyManager owner) {
		gameObject.name = "CannonEnemy";
		hp = 10;
		fireRate = .3f;
		speed = 2;
		col = gameObject.AddComponent<BoxCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,180);
		this.owner = owner;
		direction = "D";
		firingSide = "L";
		stopPosition = Random.Range (4.5f, -1f);
		var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		model = modelObject.AddComponent<CannonEnemyModel>();	
		model.init(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (hp == 0) {
			Destroy (this.gameObject);
		}
		if (transform.position.y < stopPosition) {
			direction = "L";	
			transform.position = new Vector3(transform.position.x,stopPosition,0);
		}
		if (transform.position.x <= -6) {
			direction = "R";
		}
		if (transform.position.x >= 0) {
			direction = "L";
		}
		Move ();


		if (transform.position.y == stopPosition) {
			if (cd <= 0) {
				if (firingSide == "L") {
					Fire ((transform.position.x - .25f), transform.position.y);
					firingSide = "R";
					cd = fireRate;
				}
				else if (firingSide == "R") {
					Fire ((transform.position.x + .25f), transform.position.y);
					firingSide = "L";
					cd = fireRate;
				}
			} else {
				cd = cd - Time.deltaTime;
			}
		}

	}

	void Move(){
		if (direction == "D") {
			transform.Translate (Vector3.up * Time.deltaTime * speed);
		} else if (direction == "L") {
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		} else {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		}
	}

	//I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
	protected void Fire(float x, float y){ 		
		GameObject shot = new GameObject();
		shot.transform.parent = transform.parent;
		shot.transform.position = new Vector3 (0, 0);
		shot.AddComponent<Laser> ();
		shot.transform.parent = transform.parent;
		shot.transform.position = new Vector3 (x, y);
	}

    void OnTriggerEnter2D(Collider2D other){
		if (other.name == "PlayerLaser") {
			hp--;
		}
	}
}
