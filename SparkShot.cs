using UnityEngine;
using System.Collections;

public class SparkShot : Projectile {

	public string direction;
	private GameObject spark;
	private Animator animator;

	// Use this for initialization
	void Start () {
		speed = 4;
		base.init (true, "Spark", 2f, 3f, 200);
		spark = this.gameObject;
		animator = spark.AddComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Spark_Animation_Controller");
		transform.localScale = new Vector3 (1.75f, 1.75f, 1.75f);
		name = "Spark";
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.x < -6) {
			Destroy (gameObject);
		}
		if (transform.position.x > 0) {
			Destroy (gameObject);
		}
		Move ();
	}

	public void GiveDirection(string gdirection){
		direction = gdirection;
	}

	void Move(){
		if (direction == "R") {
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		} else if (direction == "L") {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.name == "PlayerController") {
			animator.SetTrigger ("Die");
			speed = 0;
			Destroy (this.gameObject, .2f);
		}
	}
}
