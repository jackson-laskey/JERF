using UnityEngine;
using System.Collections;

public class Boss : CannonEnemy {

	public AudioClip BeamSound;
	public AudioClip explosion;
	public AudioSource audio;
	public bool dead = false;

	private Material lMat;
	private Material rMat;
	private float shotTimer;
	private char beingFired;
	private bool inTransition;

	// is an interval
	private float laserFireRate = .4f;

	// is an interval
	private float beamFireRate = .8f;
	// is a length of time
	private float beamResetTime = .3f;

	private float sparkEnemyChargeTime = 2.2f;

	private float speed = -1;

	private Color laserColor = new Color (.1f, .8f, 0);
	private Color beamColor = new Color (.8f, .1f, 0);
	private Color sparkColor = new Color (0, .2f, .8f);


	public void init(EnemyManager owner) {
		base.init (owner, 4f);
		gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "SectorStatus";
		gameObject.transform.localScale = new Vector3 (4, 4, 1);
		hp = 5;
		shotTimer = 0;
		beingFired = 'L';
		inTransition = false;
		lMat = LiCannon.GetComponent<Renderer> ().material;
		rMat = RiCannon.GetComponent<Renderer> ().material;
		lMat.color = laserColor;
		rMat.color = laserColor;

		BeamSound = Resources.Load ("Sounds/beamSound") as AudioClip;
		explosion = Resources.Load ("Sounds/explosion") as AudioClip;

		audio = gameObject.AddComponent<AudioSource> ();
		audio.loop = true;
		audio.clip = BeamSound;
		audio.volume = .2f;
		audio.Play();

//		cSprites = Resources.LoadAll<Sprite> ("Textures/Cannons_Sprite_Sheet");
//		Lcannon = new GameObject ();
//		Lcannon.transform.parent = this.transform;
//		Lcannon.transform.localPosition = new Vector3 (.305f, -.364f, 0);
//		Lcannon.transform.localScale = new Vector2 (1, 1);
//		Lcannon.AddComponent<SpriteRenderer> ();
//		Lcannon.GetComponent<SpriteRenderer> ().sprite = cSprites [0];
//		Lcannon.name = "LeftCannonOutline";
//		LiCannon = new GameObject ();
//		LiCannon.transform.parent = Lcannon.transform;
//		LiCannon.transform.localPosition = new Vector3 (0, 0, 0);
//		LiCannon.transform.localScale = new Vector2 (1, 1);
//		LiCannon.AddComponent<SpriteRenderer> ();
//		LiCannon.GetComponent<SpriteRenderer> ().sprite = cSprites[1];
//		LiCannon.name = "LeftCannonColor";
//
//
//		Rcannon = new GameObject ();
//		Rcannon.transform.parent = this.transform;
//		Rcannon.transform.localPosition = new Vector3 (-.305f, -.364f, 0);
//		Rcannon.transform.localScale = new Vector2 (1, 1);
//		Rcannon.AddComponent<SpriteRenderer> ();
//		Rcannon.GetComponent<SpriteRenderer> ().sprite = cSprites [0];
//		Rcannon.name = "RightCannonOutline";
//		RiCannon = new GameObject ();
//		RiCannon.transform.parent = Rcannon.transform;
//		RiCannon.transform.localPosition = new Vector3 (0, 0, 0);
//		RiCannon.transform.localScale = new Vector2 (1, 1);
//		RiCannon.AddComponent<SpriteRenderer> ();
//		RiCannon.GetComponent<SpriteRenderer> ().sprite = cSprites[1];
//		RiCannon.name = "RightCannonColor";
//
//
//		Lcannon.GetComponent<SpriteRenderer> ().sortingOrder = 1;
//		LiCannon.GetComponent<SpriteRenderer> ().sortingOrder = 1;
//		Rcannon.GetComponent<SpriteRenderer> ().sortingOrder = 1;
//		RiCannon.GetComponent<SpriteRenderer> ().sortingOrder = 1;
	}

	void Update() {

		if (hp <= 0) {
			animator.SetBool ("Damaged", false);
			laserFireRate = 100;
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
		if (transform.position.x <= -4.78f) {
			direction = "R";
		}
		if (transform.position.x >= -1.7f) {
			direction = "L";
		}
		Move ();


		animator.SetBool ("Moving", false);
		if (beingFired == 'L') {
			if (shotTimer >= laserFireRate) {
				if (firingSide == "L") {
					Firing ("L");
					SetCannonsFor ("R");
				} else if (firingSide == "R") {
					Firing ("L");
					SetCannonsFor ("L");
					SetProjectile ();
				}
				shotTimer = 0;
			} else {
				shotTimer += Time.deltaTime;
			}
		} else if (beingFired == 'S') {
			if (shotTimer >= sparkEnemyChargeTime) {
				if (firingSide == "L") {
					Firing ("S");
					SetCannonsFor ("R");
				} else if (firingSide == "R") {
					Firing ("S");
					SetCannonsFor ("L");
					SetProjectile ();
				}
				shotTimer = 0;
			} else {
				shotTimer += Time.deltaTime;
			
			}
		} else if (beingFired == 'B') {
			if (shotTimer >= beamFireRate) {
				if (firingSide == "L") {
					Firing ("B");
					SetCannonsFor ("R");
				} else if (firingSide == "R") {
					Firing ("B");
					SetCannonsFor ("L");
					SetProjectile ();
				}
				shotTimer = 0;
			} else {
				shotTimer += Time.deltaTime;
			}
		}
	}


	private void SetCannonsFor(string position) {
		firingSide = position;
		if (position == "C") {
			LiCannon.transform.localPosition = new Vector3 (0, 0, 0);
			RiCannon.transform.localPosition = new Vector3 (0, 0, 0);
		} else if (position == "L") {
			Rcannon.transform.localPosition = new Vector3 (-.305f, -.33f, 0);
			Lcannon.transform.localPosition = new Vector3 (.305f, -.364f, 0);
		} else if (position == "R") {
			Lcannon.transform.localPosition = new Vector3 (.305f, -.33f, 0);
			Rcannon.transform.localPosition = new Vector3 (-.305f, -.364f, 0);
		}
	}
		

	private void Firing(string type) {
		if (type == "L") {
			AudioSource.PlayClipAtPoint (LaserSound, transform.position);
			GameObject shot = new GameObject ();
			shot.transform.position = new Vector3 (0, 0);
			shot.transform.localScale = new Vector3 (2f, 2f, 1);
			shot.AddComponent<Laser> ();
			if (firingSide == "R") {
				shot.transform.position = new Vector3 (transform.position.x + (transform.localScale.x*.3f), transform.position.y - (transform.localScale.y*.42f));
			} else {
				shot.transform.position = new Vector3 (transform.position.x - (transform.localScale.x*.3f), transform.position.y - (transform.localScale.y*.42f));
			}
		} else if (type == "S") {
			GameObject enemyObject = new GameObject ();
			SparkEnemy enemy = enemyObject.AddComponent<SparkEnemy> ();
			enemy.transform.position = new Vector3 (0, 0, 0);
			enemy.init (FindObjectOfType<EnemyManager>());
			enemy.transform.parent = transform.parent;
			if (firingSide == "R") {
				enemy.transform.position = new Vector3 (transform.position.x + (transform.localScale.x*.3f), transform.position.y - (transform.localScale.y*.42f));
			} else {
				enemy.transform.position = new Vector3 (transform.position.x - (transform.localScale.x*.3f), transform.position.y - (transform.localScale.y*.42f));
			}
		} else if (type == "B") {
			AudioSource.PlayClipAtPoint (LaserSound, transform.position);
			GameObject beamShot = new GameObject();
			beamShot.transform.parent = transform;
			beamShot.name = "BeamShot";
			if (firingSide == "R") {
				beamShot.AddComponent<Beam> ().init (beamResetTime, transform.localScale.x*.076f, 0-transform.localScale.y*.028f);
			} else {
				beamShot.AddComponent<Beam> ().init (beamResetTime, transform.localScale.x*-.076f, 0-transform.localScale.y*.028f);
			}
		}
	}

	private void SetProjectile() {
		int rand = UnityEngine.Random.Range (0, 100);
		if (hp > 80) {
			beingFired = 'L';
		} else if (hp > 60) {
			if (rand > 85) {
				beingFired = 'B';
			} else {
				beingFired = 'L';
			}
		} else if (hp > 35) {
			if (rand > 90) {
				beingFired = 'S';
			} else if (rand > 65) {
				beingFired = 'B';
			} else {
				beingFired = 'L';
			}
		} else {
			if (rand > 85) {
				beingFired = 'S';
			} else if (rand > 60) {
				beingFired = 'B';
			} else {
				beingFired = 'L';
			}
		}
		switch (beingFired) {
		case 'L':
			lMat.color = laserColor;
			rMat.color = laserColor;
			break;
		case 'B':
			lMat.color = beamColor;
			rMat.color = beamColor;
			break;
		case 'S':
			lMat.color = sparkColor;
			rMat.color = sparkColor;
			break;
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.name == "PlayerLaser") {
			hp--;
			animator.SetBool ("Damaged", true);
		}
		if (coll.name == "SuperPlayerLaser") {
			hp -= 2;
			animator.SetBool ("Damaged", true);
		}
		if (hp <= 0) {
			beingFired = 'L';
		} else if (hp < 35) {
			speed = -2.8f;
		} else if (hp < 60) {
			speed = -2.1f;
		} else if (hp < 80) {
			speed = -1.6f;
		}
	}

	void Die(){
		if (!dead) {
			AudioSource.PlayClipAtPoint (burst, transform.position);
			dead = true;
		}
		animator.SetTrigger ("Die");
		animator.speed = .5f;
		Destroy (Rcannon.gameObject);
		Destroy (RiCannon.gameObject);
		Destroy (Lcannon.gameObject);
		Destroy (LiCannon.gameObject);
		Destroy (this.gameObject, 2.3f);
	}

}
