using UnityEngine;
using System.Collections;

public class Beam : Projectile {

	public float xScale;
	public float yScale;
	public int pixels;
	private GameObject laser;
	private Animator animator;

	float shotThreshold;

	// Use this for initialization
	public void init (float shotLength) {
		//Parameters
		shotThreshold = shotLength;

		xScale = 1f;
		yScale = 1;
		pixels = 200;


		base.init (true, "Laser", xScale, yScale, pixels);
		GameObject[] beams = new GameObject[14];
		beams [0] = gameObject;
		for (float i = 1; i < 14; i++) {
			beams[(int)i] = new GameObject ();
			FindObjectOfType<GameController> ().MakeSprite (beams [(int)i], "Laser", transform.parent, 0, 0 - (i/2), xScale, yScale, 200);
			beams[(int)i].name = "Beam";
			beams[(int)i].AddComponent<BoxCollider2D> ().isTrigger = true;
			beams[(int)i].GetComponent<BoxCollider2D> ().size = new Vector2 (colliderSize, colliderSize);
		}
		transform.eulerAngles  = new Vector3(0, 0, 180);
		laser = this.gameObject;
		animator = laser.AddComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Cannon_Projectile_Animation_Controller");
		name = "Beam";
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "PlayerController") {
			speed = 0;
			animator.SetTrigger ("Hit");
			Destroy (this.gameObject, .2f);
		}
	}
}
