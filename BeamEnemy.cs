using UnityEngine;
using System.Collections;

public class BeamEnemy : ParentEnemy {

	private BeamEnemyModel model;
	private float stopPosition;
	private string direction;
	public bool charging;
	public float charge;
	public float fullCharge;
	public float fireTime;
	private int fired;
	private bool retreating;
	public bool entering;


	public void init(EnemyManager owner) {
		name = "BeamEnemy";
		hp = 5;
		speed = 1;
		entering = true;
		col = gameObject.AddComponent<BoxCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,180);
		this.owner = owner;
		direction = "D";
		charging = false;
		fired = 0;
		charge = 0;
		fullCharge = 4;
		stopPosition = 4.5f;
		fireTime = 2;
		var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		model = modelObject.AddComponent<BeamEnemyModel>();	
		model.init(this);
	}

	// Update is called once per frame
	void Update () {
		if (hp <= 0) {
			Destroy (this.gameObject);
		}
		if (transform.position.y > 7 && retreating) {
			Destroy (this.gameObject);
		}
		if (transform.position.y < stopPosition) {
			entering = false;
			charging = true;
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

		if (fired >= 3 && charging) {
			retreating = true;
		}

		if (charging) {
			charge = charge + Time.deltaTime;
		}
		if (charge >= fullCharge) {
			charging = false;
			charge = 0;
			Fire ();
		}
		if (!charging) {
			fireTime = fireTime - Time.deltaTime;
			if (fireTime <= 0) {
				charging = true;
				fireTime = 2;
				StopFire ();
			}
		}

	}

	void Move(){
		if (direction == "D") {
			transform.Translate (Vector3.up * Time.deltaTime * speed);
		} else if (direction == "L") {
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		} else if (direction == "R") {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		} else if (retreating) {
			transform.Translate (Vector3.down * Time.deltaTime * speed);
		}
	
	}


	protected void Fire(){ 						//I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
		col.size = new Vector2(1,20);
		fired++;
	}

	protected void StopFire(){
		col.size = new Vector2(1,1);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "PlayerLaser") {
			hp--;
		}
		if (other.name == "SuperPlayerLaser") {
			hp -= 2;
		}
	}
}

