using UnityEngine;
using System.Collections;

public class CannonEnemy : ParentEnemy {

	private CannonEnemyModel model;
	private int stopPosition;
	private string direction;
	private string firingSide;

	void init(EnemyManager owner) {
		hp = 5;
		fireRate = 2;
		speed = 3;
		col = new Collider2D();
		body = new Rigidbody2D();
		transform.eulerAngles = new Vector3(0,0,180);
		this.owner = owner;
		direction = "D";
		firingSide = "L";
		stopPosition = Random.Range (6, -1);
		var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		model = modelObject.AddComponent<CannonEnemyModel>();	
		model.init(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < stopPosition) {
			direction = "L";	
			transform.position = new Vector3(transform.position.x,stopPosition,0);
		}
		if (transform.position.x <= -5) {
			direction = "R";
		}
		if (transform.position.x >= 5) {
			direction = "L";
		}
		Move ();

		if (transform.position.y == stopPosition) {
			if (firingSide == "L") {
				Fire ((transform.position.x - .25f), transform.position.y);
				firingSide = "R";
			}
			if (firingSide == "R") {
				Fire ((transform.position.x + .25f), transform.position.y);
				firingSide = "L";
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
		Laser laser = bulletObject.AddComponent<Laser>();
		laser.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
		laser.init(true);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerLaser") {
			hp--;
		}
	}

}
