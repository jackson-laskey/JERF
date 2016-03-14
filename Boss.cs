using UnityEngine;
using System.Collections;

public class Boss : CannonEnemy {


	private Material mat;

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
		mat = iCannon.GetComponent<Renderer> ().material;
		mat.color = new Color (.8f, 0, 0);
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

			cannon.GetComponent<SpriteRenderer> ().sprite = cSprites [oSprite];
			iCannon.GetComponent<SpriteRenderer> ().sprite = cSprites[iSprite];
			animator.SetBool ("Moving", false);
			if (shotTimer >= laserFireRate) {
				if (firingSide == "L") {
					FiringLasers ();
					firingSide = "R";
					shotTimer = 0;
				}
				else if (firingSide == "R") {
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
		if (oSprite == 15 && iSprite == 0) {
			oSprite = 16;
			iSprite = 1;
		} else {
			oSprite = 15;
			iSprite = 0;
		}
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

}
