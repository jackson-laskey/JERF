using UnityEngine;
using System.Collections;

public class ComponentHealth : MonoBehaviour {

<<<<<<< HEAD
=======
	public GameController controller;

>>>>>>> PrefabsToCode
	public float repairRate;
	public float decayModifier;
	public float health;

<<<<<<< HEAD
	public bool decaying;



	// Use this for initialization
	void Start () {
		decaying = true;
		health = 100;
		repairRate = 15f;
		decayModifier = 4.3f;
=======
	Animator animator;

	private int type;
	public bool decaying;

	public GameObject model;
	public Material mat;
	public Vector3 scale;
	Sprite[] stextures;

	private bool initd;


	// Use this for initialization
	public void init (GameController gCont, float x, float y, int type) {
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
		controller.MakeSprite (model, stextures[6], transform, x, y, 1, .92f, 200, .5f, 0);
		model.GetComponent<SpriteRenderer> ().sortingLayerName = "BottomRhsUI";
		model.transform.localPosition = new Vector3 (0, -0.623f, 0);
		mat = model.GetComponent<Renderer> ().material;
		mat.color = new Color (0, .75f, 0);

		GameObject outlineModel = new GameObject ();
		controller.MakeSprite (outlineModel, stextures[7], transform, 0, 0, 1, 1, 200, .5f, 0);
		outlineModel.GetComponent<SpriteRenderer> ().sortingLayerName = "TopRhsUI";

		decaying = true;
		health = 100;
		repairRate = 30f;
		decayModifier = 4.3f;

		initd = true;
>>>>>>> PrefabsToCode
	}

	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		if (decaying) {
			if (health <= 1) {
				gameObject.transform.localScale = new Vector3(1, 0);
				health = 0;
			} else {
				gameObject.transform.localScale = new Vector3 (1, health/100f);
				health -= (Time.deltaTime * repairRate) /decayModifier;
			}
		} else {
			if (health >= 99) {
				gameObject.transform.localScale = new Vector3 (1, 1);
				health = 100;
			} else {
				gameObject.transform.localScale = new Vector3 (1, health/100f);
				health += Time.deltaTime * repairRate;
			}
		}
		if (health > 50) {
			gameObject.GetComponent<SpriteRenderer>().color = new Color((100-health)*.015f, .75f, 0);
		} else {
			gameObject.GetComponent<SpriteRenderer>().color = new Color(.75f, health*.015f, 0);
=======
		if (!initd) {
			return;
		}


		/* 0 = laser;
		 * 1 = engine;
		 * 2 = shield;
		 * */


		if (decaying) {
			if (health <= 1) {
				if (type == 0) {
					animator.SetInteger ("Power", 0);
				} else if (type == 1) {
					animator.SetInteger ("Power", 0);
				} else {
					animator.SetInteger ("Power", 0);
					GameObject.Find ("HyperShield").GetComponent<SpriteRenderer> ().enabled = false;
				}
				model.transform.localScale = new Vector3(1, 0);
				//model.transform.localPosition = new Vector3 (model.transform.localPosition.x, model.transform.localPosition.y + ((health/100f)/50));
				health = 0;

				
			}else {
				model.transform.localScale = new Vector3 (1, .92f * (health/100f));
				//model.transform.localPosition = new Vector3 (model.transform.localPosition.x, model.transform.localPosition.y - ((health/100f)/50));
				health -= (Time.deltaTime * repairRate) /decayModifier;
				if (type == 0) {
					animator.SetInteger ("Power", 1);
				} else if (type == 1) {
					animator.SetInteger ("Power", 1);
				} else {
					animator.SetInteger ("Power", 1);
					GameObject.Find ("HyperShield").GetComponent<SpriteRenderer> ().enabled = false;
				}

			}
		} else {
			if (health >= 99) {
				model.transform.localScale = new Vector3 (1, .92f);
				//model.transform.localPosition = new Vector3 (model.transform.localPosition.x, model.transform.localPosition.y - ((health/100f)/50));
				health = 100;
				if (type == 0) {
					animator.SetInteger ("Power", 2);
				} else if (type == 1) {
					animator.SetInteger ("Power", 2);
				} else {
					animator.SetInteger ("Power", 2);
					GameObject.Find ("HyperShield").GetComponent<SpriteRenderer> ().enabled = true;
				}
			} else if (health >= 90) {
				model.transform.localScale = new Vector3 (1, .92f* (health/100f));
				//model.transform.localPosition = new Vector3 (model.transform.localPosition.x, model.transform.localPosition.y - ((health/100f)/50));
				//health = 100;
				health += Time.deltaTime * repairRate;
				if (type == 0) {
					animator.SetInteger ("Power", 2);
				} else if (type == 1) {
					animator.SetInteger ("Power", 2);
				} else {
					animator.SetInteger ("Power", 2);
					GameObject.Find ("HyperShield").GetComponent<SpriteRenderer> ().enabled = true;
				}

			}
			else if (health < 90){
				model.transform.localScale = new Vector3 (1, .92f * (health/100f));
				//model.transform.localPosition = new Vector3 (model.transform.localPosition.x, model.transform.localPosition.y + ((health/100f)/50));
				health += Time.deltaTime * repairRate;
				if (type == 0) {
					animator.SetInteger ("Power", 1);
				} else if (type == 1) {
					animator.SetInteger ("Power", 1);
				} else {
					animator.SetInteger ("Power", 1);
					GameObject.Find ("HyperShield").GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
		}


		if (health > 50) {
			mat.color = new Color((100-health)*.015f, .75f, 0);
		} else {
			mat.color = new Color(.75f, health*.015f, 0);
>>>>>>> PrefabsToCode
		}
	}

	// damages the part and returns true if it lowers the part's health to 0
	public bool Damage(float damage) {
		float damageModifier = 1;
		if (health == 100) {
			damageModifier = .6f;
		} else if (!decaying) {
			damageModifier = .75f;
		}
		if (health - (damage * damageModifier) <= 0) {
			health = 0;
			return true;
		} else {
			health -= damage * damageModifier;
			return false;
		}
	}
}
