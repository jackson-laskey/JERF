using UnityEngine;
using System.Collections;

public class Asteroid : ParentEnemy {
	

	CircleCollider2D collid;



	private float sizex = .65f;
	private float sizey = .65f;
	private float bottomEdge = -7f;
	private Sprite[] astroids;

	public void init(EnemyManager owner) {
		hp = 20;
		speed = -2;
		this.owner = owner;
		astroids = Resources.LoadAll<Sprite> ("Textures/Asteroid_Sprite_Sheet");
		transform.localScale = new Vector3 (Random.Range(sizex,sizex+.25f), Random.Range(sizey,sizey+.25f), 1);
		SpriteRenderer rend = gameObject.AddComponent<SpriteRenderer> ();
		rend.sprite = astroids [0];
		animator = gameObject.AddComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Asteroid_Animation_Controller");
		col = this.gameObject.AddComponent<PolygonCollider2D> ();
		body = this.gameObject.AddComponent<Rigidbody2D> ();
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,0);
		this.name = "Asteroid";
		this.tag = "asteroid";
	}


	void Update () {
		if (transform.position.y < bottomEdge) {
			Destroy (this.gameObject);
		}
		if (hp <= 0) {
			speed = 0;
			animator.SetTrigger ("Die");
			Destroy (this.gameObject, .4f);
		}
		Move ();
	}

	void Move(){
		transform.Translate (Vector3.up * Time.deltaTime * speed);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerLaser") {
			hp--;
		}
		if (other.tag == "PlayerController") {
			speed = 0;
			animator.SetTrigger ("Die");
			Destroy (this.gameObject, .4f);


		}
	}
}
