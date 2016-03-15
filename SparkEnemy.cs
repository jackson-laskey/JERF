﻿using UnityEngine;
using System.Collections;

public class SparkEnemy : ParentEnemy {

	private bool fired;


	private float bottomEdge = -7f;
	private float sizex = .65f;
	private float sizey = .65f;
	private GameObject spark;
	private Animator sAnimator;
	private float dmgCount = .3f;
	private Sprite[] SE;
	SpriteRenderer rend;

	public AudioClip Spark;
	public AudioClip sparkLoop;
	public AudioClip explosion;
	public AudioSource audio;
	public bool dead = false;

	private bool isBoss;

	public void init(EnemyManager owner, params bool[] boss) {
		if (boss.Length > 0) {
			isBoss = true;
		} else {
			isBoss = false;
		}
		hp = 5;
		speed = -1;
		transform.localScale = new Vector3 (sizex, sizey, 1);
		rend = gameObject.AddComponent<SpriteRenderer> ();
		SE = Resources.LoadAll<Sprite> ("Textures/Spark Enemy Sprite Sheet");
		rend.sprite = SE[13];
		col = gameObject.AddComponent<PolygonCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		animator = gameObject.AddComponent<Animator> ();
		this.name = "Spark Enemy";
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Spark_Enemy_Animation_Controller");
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,0);
		this.transform.localScale = new Vector2 (1.5f, 1.5f);
		this.owner = owner;
		spark = new GameObject ();
		spark.name = "Spark Core";
		spark.AddComponent<SpriteRenderer> ();
		sAnimator = spark.gameObject.AddComponent<Animator> ();
		sAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Spark_Animation_Controller");
		spark.transform.parent = this.transform;
		animator.SetFloat ("MoveRate", .7f);
		animator.SetBool ("Damaged", false);
		spark.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		spark.transform.localScale = new Vector3 (1, 1);
		spark.transform.localPosition = new Vector3 (0, 0, 0);


		fired = false;

		Spark = Resources.Load ("Sounds/spark") as AudioClip;
		explosion = Resources.Load ("Sounds/burst") as AudioClip;
		sparkLoop = Resources.Load ("Sounds/sparkLoop") as AudioClip;

		audio = gameObject.AddComponent<AudioSource> ();
		audio.loop = true;
		audio.clip = sparkLoop;
		audio.volume = .5f;
		audio.Play();
	}

	void Update () {
		if (hp <= 0) {
			speed = 0;
			animator.SetBool ("Damaged", false);
			Die ();
		}

		if (animator.GetBool ("Damaged")) {
			dmgCount = dmgCount - Time.deltaTime;
		}
		if (dmgCount <= 0) {
			animator.SetBool ("Damaged", false);
			dmgCount = .3f;
		}

		Move ();
		float playery = owner.owner.ship.transform.position.y;

		if (playery >= this.transform.position.y) {
			if (!fired) {
				fired = true;
				Fire ("L");
				Fire ("R");
				spark.gameObject.SetActive (false);
			}
		}

		if (transform.position.y < bottomEdge) {
			Destroy (this.gameObject);
		} 
	}

	void Move(){
		transform.Translate (Vector3.up * Time.deltaTime * speed);
	}

	protected void Fire(string direction){  //I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
		GameObject shot1 = new GameObject();
		shot1.transform.parent = transform.parent;
		shot1.transform.position = new Vector3 (0, 0);
		SparkShot leftshot = shot1.AddComponent<SparkShot> ();
		leftshot.GiveDirection (direction);
		shot1.transform.parent = transform.parent;
		shot1.transform.position = new Vector3 (transform.position.x, transform.position.y);
		AudioSource.PlayClipAtPoint (Spark, transform.position);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (!dead) {
			if (other.name == "PlayerLaser") {
				animator.SetBool ("Damaged", true);
				hp--;
			}
			if (other.name == "SuperPlayerLaser") {
				animator.SetBool ("Damaged", true);
				hp--;
			}
			if (other.tag == "PlayerController") {
				hp -= 2;
				animator.SetBool ("Damaged", true);
			}
		}
	}

	void Die(){
		if (!dead) {
			audio.Pause ();
			AudioSource.PlayClipAtPoint (explosion, transform.position);
			dead = true;
		
			if (isBoss) {
				GameObject powerup = new GameObject ();
				powerup.transform.position = new Vector3 (transform.position.x, transform.position.y);
				switch (UnityEngine.Random.Range (0, 5)) {
				case 0:
					powerup.AddComponent<PowerUp> ().init (owner, "P1");
					break;
				case 1:
					powerup.AddComponent<PowerUp> ().init (owner, "P2");
					break;
				case 2:
					powerup.AddComponent<PowerUp> ().init (owner, "P3");
					break;
				default:
					Destroy (powerup);
					break;
				}
				powerup.transform.position = new Vector3 (transform.position.x, transform.position.y);
				powerup.GetComponent<PolygonCollider2D> ().isTrigger = true;
			}
		}
		animator.SetTrigger ("Die");
		this.name = "Dead";
		spark.transform.localScale = new Vector2 (1.25f, 1.25f);
		sAnimator.SetTrigger ("Die");
		Destroy (spark.gameObject, .2f);
		Destroy (this.gameObject, 1f);
	}
		
}
