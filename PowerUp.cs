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
	//private Sprite[] SE;

	public void init(EnemyManager owner, string type) {
		name = type;
		transform.localScale = new Vector3 (sizex, sizey, 1);
		SpriteRenderer rend = this.gameObject.AddComponent<SpriteRenderer> ();
		rend.sprite = Resources.Load<Sprite>("Textures/powerUp");
		//SE = Resources.LoadAll<Sprite> ("Textures/Small_Enemy_Sprite_Sheet");
		//rend.sprite = SE [14];
		col = gameObject.AddComponent<PolygonCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		//animator = gameObject.AddComponent<Animator> ();
		//animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/SE_Animation_Controller");
		body.isKinematic = true;

		//transform.localScale = new Vector3 (1.25f, 1.25f, 0);
		this.owner = owner;
	}

	void Update () {


		if ( transform.position.y < bottomEdge){
			Destroy(this.gameObject);
		}
		Move ();
	}

	void Move(){
		transform.Translate (Vector3.up * Time.deltaTime * speed);
	}



	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerController") {
			Destroy (this.gameObject);
		}
	}


}