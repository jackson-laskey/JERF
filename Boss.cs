using UnityEngine;
using System.Collections;

public class Boss : CannonEnemy {


	private Material Lmat;
	private Material Rmat;
	private float shotTimer;
	private char beingFired;
	private bool inTransition;

	private float laserFireRate = .25f;

	private float sparkEnemyChargeTime = 1f;
	private float sparkEnemyTimeBetweenSpawns;


	public void init(EnemyManager owner) {
		base.init (owner, 4f);
		gameObject.transform.localScale = new Vector3 (3, 3, 1);
		speed = -1;
		hp = 50;
		shotTimer = 0;
		beingFired = 'L';
		inTransition = false;

		cSprites = Resources.LoadAll<Sprite> ("Textures/Cannons_Sprite_Sheet");
		Lcannon = new GameObject ();
		Lcannon.transform.parent = this.transform;
		Lcannon.transform.localPosition = new Vector3 (.305f, -.364f, 0);
		Lcannon.transform.localScale = new Vector2 (1, 1);
		Lcannon.AddComponent<SpriteRenderer> ();
		Lcannon.GetComponent<SpriteRenderer> ().sprite = cSprites [0];
		Lcannon.name = "LeftCannonOutline";
		LiCannon = new GameObject ();
		LiCannon.transform.parent = Lcannon.transform;
		LiCannon.transform.localPosition = new Vector3 (0, 0, 0);
		LiCannon.transform.localScale = new Vector2 (1, 1);
		LiCannon.AddComponent<SpriteRenderer> ();
		LiCannon.GetComponent<SpriteRenderer> ().sprite = cSprites[1];
		LiCannon.name = "LeftCannonColor";
		Lmat = LiCannon.GetComponent<Renderer> ().material;
		Lmat.color = new Color (.8f, 0, 0);


		Rcannon = new GameObject ();
		Rcannon.transform.parent = this.transform;
		Rcannon.transform.localPosition = new Vector3 (-.305f, -.364f, 0);
		Rcannon.transform.localScale = new Vector2 (1, 1);
		Rcannon.AddComponent<SpriteRenderer> ();
		Rcannon.GetComponent<SpriteRenderer> ().sprite = cSprites [0];
		Rcannon.name = "RightCannonOutline";
		RiCannon = new GameObject ();
		RiCannon.transform.parent = Rcannon.transform;
		RiCannon.transform.localPosition = new Vector3 (0, 0, 0);
		RiCannon.transform.localScale = new Vector2 (1, 1);
		RiCannon.AddComponent<SpriteRenderer> ();
		RiCannon.GetComponent<SpriteRenderer> ().sprite = cSprites[1];
		RiCannon.name = "RightCannonColor";

		Rmat = RiCannon.GetComponent<Renderer> ().material;
		Rmat.color = new Color (.8f, 0, 0);

		Lcannon.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		LiCannon.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		Rcannon.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		RiCannon.GetComponent<SpriteRenderer> ().sortingOrder = 1;
	}

	void Update() {
		if (hp <= 0) {
			animator.SetBool ("Damaged", false);
			speed = -.2f;
			Die ();
		}
		if (animator.GetBool ("Damaged")) {
			dmgCount = dmgCount - Time.deltaTime;
		}
		if (dmgCount <= 0) {
			animator.SetBool ("Damaged", false);
			dmgCount = .3f;
		}

		if (transform.position.y < stopPosition) {
			direction = "L";	
			transform.position = new Vector3(transform.position.x,stopPosition,0);

		}
		if (transform.position.x <= -5.2f) {
			direction = "R";
		}
		if (transform.position.x >= -1.3f) {
			direction = "L";
		}
		Move ();

			animator.SetBool ("Moving", false);
			if (shotTimer >= laserFireRate) {
				if (firingSide == "L") {
					Lcannon.transform.localPosition = new Vector3 (.305f, -.33f, 0);
					Rcannon.transform.localPosition = new Vector3 (-.305f, -.364f, 0);
					FiringLasers ();
					firingSide = "R";
					shotTimer = 0;
				}
				else if (firingSide == "R") {
					Rcannon.transform.localPosition = new Vector3 (-.305f, -.33f, 0);
					Lcannon.transform.localPosition = new Vector3 (.305f, -.364f, 0);
					FiringLasers ();
					firingSide = "L";
					shotTimer = 0;
				}
			} else {
				shotTimer = shotTimer + Time.deltaTime;
			}
	}


	private void FiringLasers() {
		AudioSource.PlayClipAtPoint(LaserSound, transform.position);

		GameObject shot = new GameObject();
		shot.transform.position = new Vector3 (0, 0);
		shot.transform.localScale = new Vector3 (2f, 2f, 1);
		shot.AddComponent<Laser> ();
		if (firingSide == "R") {
			shot.transform.position = new Vector3 (transform.position.x+.9f, transform.position.y-1.25f);
		} else {
			shot.transform.position = new Vector3 (transform.position.x-.9f, transform.position.y-1.25f);
		}
	}

	void Die(){
		if (!dead) {
			AudioSource.PlayClipAtPoint (burst, transform.position);
			dead = true;
		}
		Destroy (Rcannon.gameObject);
		Destroy (RiCannon.gameObject);
		Destroy (Lcannon.gameObject);
		Destroy (LiCannon.gameObject);
		animator.SetTrigger ("Die");
		Destroy (this.gameObject, 1.1f);
	}

}
