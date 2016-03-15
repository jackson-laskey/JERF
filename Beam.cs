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
	GameObject[] beams = new GameObject[16];

	// Use this for initialization
	public void init (float shotLength, params float[] position) {
		//Parameters
		shotThreshold = shotLength;

		xScale = 1f;
		yScale = 1.5f;
		pixels = 200;


		//base.init (true, "Laser", xScale, yScale, pixels);

		beams [0] = gameObject;
		float x = 0;
		float y = 0;
		if (position.Length == 2) {
			x = position [0];
			y = position [1];
			xScale = xScale / 2;
		}
		transform.localPosition = new Vector3 (x, y-(1/2), 0);
		for (float i = 1; i < 16; i++) {
			beams[(int)i] = new GameObject ();
			FindObjectOfType<GameController> ().MakeSprite (beams [(int)i], "Laser", this.transform.parent, x, y - (i * .45f), xScale, yScale, 200);
			beams[(int)i].name = "Beam";
			animator = beams[(int)i].AddComponent<Animator> ();
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Beam_Projectile_Animation_Controller");
			beams [(int)i].AddComponent<BoxCollider2D> ().isTrigger = true;
			colliderSizex = .3f;
			colliderSizey = .4f;
			beams[(int)i].GetComponent<BoxCollider2D> ().size = new Vector2 (colliderSizex, colliderSizey);
			beams [(int)i].AddComponent<Rigidbody2D> ().isKinematic = true;
			if (i < 5) {
				beams[(int)i].GetComponent<SpriteRenderer> ().sortingLayerName = "ShipButton";
			}
		}
//		animator = laser.AddComponent<Animator> ();
//		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Cannon_Projectile_Animation_Controller");
		name = "Beam1";
		transform.eulerAngles  = new Vector3(0, 0, 0);

	}

	// Update is called once per frame
	void Update () {
		base.Update ();

		if ((shotClock += Time.deltaTime) >= shotThreshold) {
			for (float i = 1; i < 16; i++) {
				Destroy(beams[(int)i]);
			}
			Destroy (this.gameObject);
		}
	}

	/*void OnTriggerEnter2D(Collider2D coll) {
		print ("hi");
		if (coll.name == "PlayerLaser") {
			this.GetComponentInParent<Beam> ().GetComponentInParent<BeamEnemy> ().hp++;
		}
		if (coll.name == "SuperPlayerLaser") {
			this.GetComponentInParent<Beam> ().GetComponentInParent<BeamEnemy> ().hp += 2;
		}
	}*/
}
