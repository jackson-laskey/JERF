using UnityEngine;
using System.Collections;

public class Beam : Projectile {

	public float xScale;
	public float yScale;
	public int pixels;
	private GameObject laser;
	private Animator animator;

	public float shotThreshold;

	public float shotClock;
	GameObject[] beams = new GameObject[14];

	// Use this for initialization
	public void init (float shotLength, params float[] position) {
		//Parameters
		shotThreshold = shotLength;

		xScale = 1f;
		yScale = 1;
		pixels = 200;


		base.init (true, "Laser", xScale, yScale, pixels);
		beams [0] = gameObject;
		float x = 0;
		float y = 0;
		if (position.Length == 2) {
			x = position [0];
			y = position [1];
		}
		transform.localPosition = new Vector3 (x, y-(1/2), 0);
		for (float i = 1; i < 14; i++) {
			beams[(int)i] = new GameObject ();
			FindObjectOfType<GameController> ().MakeSprite (beams [(int)i], "Laser", transform.parent, x, y - (i/2), xScale, yScale, 200);
			beams[(int)i].name = "Beam";
			beams[(int)i].AddComponent<BoxCollider2D> ().isTrigger = true;
			beams[(int)i].GetComponent<BoxCollider2D> ().size = new Vector2 (colliderSize, colliderSize);
		}
		transform.eulerAngles  = new Vector3(0, 0, 180);
		laser = this.gameObject;
//		animator = laser.AddComponent<Animator> ();
//		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Cannon_Projectile_Animation_Controller");
		name = "Beam1";

	}

	// Update is called once per frame
	void Update () {
		base.Update ();

		if ((shotClock += Time.deltaTime) >= shotThreshold) {
			for (float i = 1; i < 14; i++) {
				Destroy(beams[(int)i]);
			}
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "PlayerController") {
			speed = 0;
			animator.SetTrigger ("Hit");
			for (float i = 1; i < 14; i++) {
				Destroy(beams[(int)i]);
			}
			Destroy (this.gameObject, .2f);
		}
	}
}
