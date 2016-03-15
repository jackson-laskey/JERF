using UnityEngine;
using System.Collections;

public class ComponentHealth : MonoBehaviour {

	public GameController controller;

	public float repairRate;
	public float decayModifier;
	public float health;

	public bool maintain;
	public float gracePeriod;

	public bool powerUp;
	public int powerUpInt;

	Animator animator;

	private int type;
	public bool decaying;

	public GameObject model;
	public Material mat;
	public Vector3 scale;
	Sprite[] stextures;

	public AudioSource audio;

	private bool initd;

	// point at which health bars are colored and modified as if they are full
	float fullThreshold = 97;

	// fraction of damage taken when shields are manned/ above fullThreshold
	float shieldMannedModifier = .8f;
	float shieldFullModifier = .75f;

	// COLORS ARE HANDLED AT THE END OF UPDATE()


	// Use this for initialization
	public void init (GameController gCont, float x, float y, int type) {

		//Sound stuff
		audio = gameObject.AddComponent<AudioSource>();

		stextures = Resources.LoadAll<Sprite> ("Textures/Captain_Effects_Sheet_2");

		this.type = type;
		if (type == 0) {
			animator = GameObject.Find ("Cockpit").GetComponent<Animator> ();
		} else if (type == 1) {
			animator = GameObject.Find ("EngineModule").GetComponent<Animator> ();
		} else {
			animator = GameObject.Find ("ShieldModule").GetComponent<Animator> ();
		}

		controller = gCont;
		model = new GameObject();
		controller.MakeSprite (model, stextures[6], transform, x, y, .9f, .92f, 200, .5f, 0);
		model.GetComponent<SpriteRenderer> ().sortingLayerName = "BottomRhsUI";
		model.transform.localPosition = new Vector3 (0, -0.623f, 0);
		mat = model.GetComponent<Renderer> ().material;
		mat.color = new Color (0, .75f, 0);

		GameObject outlineModel = new GameObject ();
		controller.MakeSprite (outlineModel, stextures[7], transform, 0, 0, 1, 1, 200, .5f, 0);
		outlineModel.GetComponent<SpriteRenderer> ().sortingLayerName = "TopRhsUI";

		decaying = true;
		maintain = false;
		health = 100;
		if (type == 2) {
			repairRate = 18f;
			decayModifier = 2f;
		} else {
			repairRate = 25f;
			decayModifier = 3.5f;
		}

		initd = true;
	}

	// Update is called once per frame
	void Update () {

		if (powerUp) {
			if (powerUpInt == 1) {
				powerUpInt = 0;
				health = 100;
			} else {
				maintain = true;
				gracePeriod = 3;
				powerUp = false;
			}
		}

		if (!initd) {
			mat.color = new Color (0, .75f, 0);
			return;
		}


		/* 0 = laser;
		 * 1 = engine;
		 * 2 = shield;
		 * */


		if (maintain) {
			if (decaying) {
				gracePeriod = gracePeriod - Time.deltaTime;
				if (gracePeriod <= 0) {
					maintain = false;
					health = 98;
				}
			}
		} else {
			if (decaying) {
				if (health <= 1) {
					if (type == 0) {
						animator.SetInteger ("Power", 0);
					} else if (type == 1) {
						animator.SetInteger ("Power", 0);
					} else {
						animator.SetInteger ("Power", 0);
						GameObject.Find ("HyperShield").GetComponent<SpriteRenderer> ().enabled = false;
						GameObject.Find ("LeftShield").GetComponent<SpriteRenderer> ().enabled = false;
					}
					model.transform.localScale = new Vector3 (.9f, 0);
					//model.transform.localPosition = new Vector3 (model.transform.localPosition.x, model.transform.localPosition.y + ((health/100f)/50));
					health = 0;

				
				} else {
					model.transform.localScale = new Vector3 (.9f, .92f * (health / 100f));
					//model.transform.localPosition = new Vector3 (model.transform.localPosition.x, model.transform.localPosition.y - ((health/100f)/50));
					health -= (Time.deltaTime * repairRate) / decayModifier;
					if (type == 0) {
						animator.SetInteger ("Power", 1);
					} else if (type == 1) {
						animator.SetInteger ("Power", 1);
					} else {
						animator.SetInteger ("Power", 1);
						GameObject.Find ("HyperShield").GetComponent<SpriteRenderer> ().enabled = false;
						GameObject.Find ("LeftShield").GetComponent<SpriteRenderer> ().enabled = false;
					}

				}
			} else {
			
				if (health >= 99) {
					model.transform.localScale = new Vector3 (.9f, .92f);
					//model.transform.localPosition = new Vector3 (model.transform.localPosition.x, model.transform.localPosition.y - ((health/100f)/50));
					health = 100;
					maintain = true;
					gracePeriod = 1f;
					if (type == 0) {
						animator.SetInteger ("Power", 2);
					} else if (type == 1) {
						animator.SetInteger ("Power", 2);
					} else {
						animator.SetInteger ("Power", 2);
						GameObject.Find ("HyperShield").GetComponent<SpriteRenderer> ().enabled = true;
						GameObject.Find ("LeftShield").GetComponent<SpriteRenderer> ().enabled = true;
					}
				} else if (health >= 90) {
					model.transform.localScale = new Vector3 (.9f, .92f * (health / 100f));
					health += Time.deltaTime * repairRate;
					if (type == 0) {
						animator.SetInteger ("Power", 1);
					} else if (type == 1) {
						animator.SetInteger ("Power", 1);
					} else {
						animator.SetInteger ("Power", 1);
						GameObject.Find ("LeftShield").GetComponent<SpriteRenderer> ().enabled = false;
					}

				} else if (health < 90) {
					model.transform.localScale = new Vector3 (.9f, .92f * (health / 100f));
					health += Time.deltaTime * repairRate;
					if (type == 0) {
						animator.SetInteger ("Power", 1);
					} else if (type == 1) {
						animator.SetInteger ("Power", 1);
					} else {
						animator.SetInteger ("Power", 1);
						GameObject.Find ("HyperShield").GetComponent<SpriteRenderer> ().enabled = false;
						GameObject.Find ("LeftShield").GetComponent<SpriteRenderer> ().enabled = false;
					}
				}
			}
		}
			
		//
		// HANDLES COLOR/COLOR THRESHOLDS
		//
		if (type == 2) {
			if (health > fullThreshold) {
				mat.color = new Color (0, 0, 1);
			} else if (health < 30) {
				mat.color = new Color (.8f, 0, 0);
			} else if (health < 70) {
				mat.color = new Color (.75f, .75f, 0);
			} else {
				mat.color = new Color (0, .75f, 0);
			}
		}

		if (type == 1) {
			if (health > fullThreshold) {
				mat.color = new Color (1, .52f, 0);
			} else if (health < 30) {
				mat.color = new Color (.8f, 0, 0);
			} else if (health < 70) {
				mat.color = new Color (.75f, .75f, 0);
			} else {
				mat.color = new Color (0, .75f, 0);
			}
		}
	}




	// damages the part and returns true if it lowers the part's health to 0
	public bool Damage(float damage) {
		if (maintain) {
			maintain = false;
		}
		float damageModifier = 1;
		if (!decaying) {
			damageModifier = shieldMannedModifier;
		}
		if (health > fullThreshold) {
			damageModifier = shieldFullModifier;
		}
		if (health - (damage * damageModifier) <= 0) {
			health = 0;
			return true;
		} else {
			health -= damage * damageModifier;
			return false;
		}
	}

	public void PowerUp(){
		powerUp = true;
		powerUpInt = 1;
	}
}
