using UnityEngine;
using System.Collections;

public class PowerUp : ParentEnemy {


	private string direction;
	private float sizex = .65f;
	private float sizey = .65f;
	private float bottomEdge = -7f;
	public float speed = -2f;
	private Material mat;
	//private float dmgCount = .3f;
	private Sprite[] powerup;

	public void init(EnemyManager owner, string type) {
		this.owner = owner;
		name = type;
		transform.localScale = new Vector3 (sizex, sizey, 1);
		SpriteRenderer rend = this.gameObject.AddComponent<SpriteRenderer> ();
		powerup = Resources.LoadAll<Sprite> ("Textures/Power_UP_Sprite_Sheet");
		rend.sprite = powerup [0];
		col = gameObject.AddComponent<PolygonCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		animator = gameObject.AddComponent<Animator> ();
		if (type == "P1") {//shield
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Shield_PowerUP_Animation_Controller");
		} else if (type == "P2") {//engine
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Engine_PowerUP_Animation_Controller");
		} else if (type == "P3"){//laser
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Laser_PowerUP_Animation_Controller");
		}

		body.isKinematic = true;
		col.isTrigger = true;

		//transform.localScale = new Vector3 (1.25f, 1.25f, 0);
	}

	void Update () {
		col.isTrigger = true;
		if ( transform.position.y < bottomEdge){
			Destroy(this.gameObject);
		}
		Move ();
	}

	void Move(){
		transform.Translate (Vector3.up * Time.deltaTime * speed);
	}



	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Ship") {
			print ("hello");
			Destroy (this.gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.name == "Ship") {
			Destroy (this.gameObject);
		}
	}


}