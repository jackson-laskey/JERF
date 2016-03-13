﻿using UnityEngine;
using System.Collections;

public class BeamEnemy : ParentEnemy {

	private float stopPosition;
	private string direction;
	public bool charging;
	public float charge;
	public int fired;
	public bool retreating;
	public bool entering;
	public float fireTime;
	private float dmgCount;

	private GameObject beam;
	private Animator bAnimator;


	public float fullCharge = 3;
	public float fireTimeReset = 2;
	private float sizex = .65f;
	private float sizey = .65f;

	public void init(EnemyManager owner) {
		hp = 5;
		speed = -1;
		charging = false;
		transform.localScale = new Vector3 (sizex, sizey, 1);
		entering = true;
		SpriteRenderer rend = gameObject.AddComponent<SpriteRenderer> ();
		animator = gameObject.AddComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/BE_Animation_Controller");
		col = this.gameObject.AddComponent<PolygonCollider2D> ();
		body = this.gameObject.AddComponent<Rigidbody2D> ();
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,0);
		transform.localScale = new Vector2 (1.5f, 1.5f);
		this.owner = owner;
		direction = "D";
		animator.SetBool ("Charging", false);
		animator.SetFloat ("ChargeRate", 0f);
		animator.SetBool ("Fire", false);
		fired = 0;
		charge = 0;
		stopPosition = 4.55f;
		this.name = "BeamEnemy";
		dmgCount = .3f;

		beam = new GameObject ();
		beam.AddComponent<SpriteRenderer> ();
		bAnimator = beam.AddComponent<Animator> ();
		bAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Beam_Charge_Animation_Controller");
		beam.transform.parent = this.transform;
		beam.transform.localPosition = new Vector3 (0, -.148f, 0);
		beam.transform.localScale = new Vector2 (1, 1);
		bAnimator.SetBool ("Charging", false);
		bAnimator.SetFloat ("ChargeRate", 0f);
		bAnimator.SetBool ("Fire", false);
		beam.name = "Beam";


	}

	// Update is called once per frame
	void Update () {
		if (hp <= 0) {
			Die ();
		}
		if (animator.GetBool ("Damaged")) {
			dmgCount = dmgCount - Time.deltaTime;
		}
		if (dmgCount <= 0) {
			animator.SetBool ("Damaged", false);
			dmgCount = .3f;
		}
		if (transform.position.y > 7 && retreating) {
			Destroy (this.gameObject);
		}
		if (entering) {
			if (transform.position.y < stopPosition) {
				entering = false;
				animator.SetBool ("Charging", true);
				bAnimator.SetBool ("Charging", true);
				charging = true;
				direction = "L";	
				transform.position = new Vector3 (transform.position.x, stopPosition, 0);
			}
			Move ();
		} else {
			if (transform.position.x <= -6) {
				direction = "R";
			}
			if (transform.position.x >= -1) {
				direction = "L";
			}
			Move ();

			if (charging) {
				charge = charge + Time.deltaTime;
				animator.SetFloat ("ChargeRate", charge);
				bAnimator.SetFloat ("ChargeRate", charge);
			}
			if (charge >= fullCharge) {
				charging = false;
				animator.SetBool ("Charging", false);
				bAnimator.SetBool ("Charging", false);
				charge = 0;
				animator.SetFloat ("ChargeRate", 0f);
				bAnimator.SetFloat ("ChargeRate", 0f);
				fireTime = fireTimeReset;
				animator.SetBool ("Fire", true);
				bAnimator.SetBool ("Fire", true);
				Fire ();
			}
			if (!charging) {
				fireTime = fireTime - Time.deltaTime;
				if (fireTime <= 0) {
					charging = true;
					animator.SetBool ("Fire", false);
					bAnimator.SetBool ("Fire", false);
					animator.SetBool ("Charging", true);
					bAnimator.SetBool ("Charging", true);
				}
			}
		}

	}

	void Move(){
		if (retreating) {
			transform.Translate (Vector3.down * Time.deltaTime * speed);
		}else if (direction == "D") {
			transform.Translate (Vector3.up * Time.deltaTime * speed);
		} else if (direction == "L") {
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		} else if (direction == "R") {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		} 
	
	}


	protected void Fire(){ 		
		// Ryan fill this in to fire the same beam as the
		// Make the Beam last as long as FireTimeReset at the top of this code

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "PlayerLaser") {
			hp--;
			animator.SetBool ("Damaged", true);
		}
		if (other.name == "SuperPlayerLaser") {
			hp -= 2;
			animator.SetBool ("Damaged", true);
		}
	}

	void Die(){
		speed = 0;
		Destroy (beam.gameObject);
		animator.SetTrigger ("Die");
		Destroy (this.gameObject, .6f);

	}
}

