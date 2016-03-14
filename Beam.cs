using UnityEngine;
using System.Collections;

public class Beam : Projectile {

	public float xScale;
	public float yScale;
	public int pixels;
	private GameObject laser;
	private Animator animator;

	float shotThreshold;

	float shotClock;
	GameObject[] beams = new GameObject[16];

	// Use this for initialization
	public void init (float shotLength) {
		//Parameters
		shotThreshold = shotLength;

		xScale = 1f;
		yScale = 1.5f;
		pixels = 200;


		base.init (true, "Laser", xScale, yScale, pixels);

		beams [0] = gameObject;
		for (float i = 1; i < 16; i++) {
			beams[(int)i] = new GameObject ();
			FindObjectOfType<GameController> ().MakeSprite (beams [(int)i], "Laser", transform.parent, 0, 0 - (i * .45f), xScale, yScale, 200);
			beams[(int)i].name = "Beam";
			animator = beams[(int)i].AddComponent<Animator> ();
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Beam_Projectile_Animation_Controller");
			beams[(int)i].AddComponent<BoxCollider2D> ().isTrigger = true;
			beams[(int)i].GetComponent<BoxCollider2D> ().size = new Vector2 (colliderSize, colliderSize);
		}
		transform.eulerAngles  = new Vector3(0, 0, 0);
		name = "Beam";

	}

	// Update is called once per frame
	void Update () {
		base.Update ();

		if ((shotClock += Time.deltaTime) >= shotThreshold) {
			for (float i = 1; i < 16; i++) {
				Destroy(beams[(int)i]);
			}
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "PlayerController") {
		}
	}
}
