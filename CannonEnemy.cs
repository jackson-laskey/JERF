using UnityEngine;
using System.Collections;

public class CannonEnemy : ParentEnemy {

	private CannonEnemyModel model;
	private int stopPosition;
	private string direction;
	private string firingSide;

	private float cd;

	public void init(EnemyManager owner) {
		hp = 5;
		fireRate = 1;
		speed = 2;
		col = gameObject.AddComponent<BoxCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,180);
		this.owner = owner;
		direction = "D";
		firingSide = "L";
		stopPosition = Random.Range (5, -1);
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


	protected void Fire(float x, float y){ 						//I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
		GameObject bulletObject = new GameObject();		
		GameObject laser = Instantiate(Resources.Load("Prefabs/Laser") as GameObject);
		laser.transform.position = new Vector3 (x, y);
		//Laser laser = bulletObject.AddComponent<Laser>();
		//laser.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
		//laser.init(true);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerLaser") {
			hp--;
		}
	}

}
