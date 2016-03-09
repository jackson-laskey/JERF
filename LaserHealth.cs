using UnityEngine;
using System.Collections;

public class LaserHealth : MonoBehaviour {
	
	public GameController controller;

	public float repairRate = 30f;
	public float health = 100;
	public float fireCost = 7;
	
	public GameObject model;
	public Material mat;
	public Vector3 scale;
	Sprite[] stextures;
	
	public bool decaying;
	
	private bool initd;

	private int type;
	private Animator animator;
	private float slowDownThreshold = 85f;
	private float slowDownModifier = .4f;

	private bool flashing;
	private float flashIncrement = .00041f;
	// max speed at which the alpha will change per frame
	private float maxFlashSpeed = 0.0125f;
	// the rate at which the alpha is currently changing
	private float flashRvalue = 1f;
	private float flashGvalue = .82f;
	private float currFlashSpeed;
	private float initialAlpha;
	
		// Use this for initialization
	public void init (GameController gCont, float x, float y, int type) {
		currFlashSpeed = 0;
		stextures = Resources.LoadAll<Sprite> ("Textures/Captain_Effects_Sheet_2");
		this.type = type;
		animator = GameObject.Find ("Cockpit").GetComponent<Animator> ();
		controller = gCont;
		model = new GameObject();
		controller.MakeSprite (model, stextures[6], transform, x, y, .9f, .92f, 200, .5f, 0);
//		controller.MakeSprite (model, "Bar", transform, x, y, 1, 1, 200, .5f, 0);
		model.GetComponent<SpriteRenderer> ().sortingLayerName = "BottomRhsUI";
		model.transform.localPosition = new Vector3 (0, -0.623f, 0);
		mat = model.GetComponent<Renderer> ().material;
		mat.color = new Color (0, .75f, 0);

		initialAlpha = mat.color.a;
		
		GameObject outlineModel = new GameObject ();
		controller.MakeSprite (outlineModel, stextures[7], transform, 0, 0, 1, 1, 200, .5f, 0);
//		controller.MakeSprite (outlineModel, "BarOutline", transform, 0, -.03f, 1, 1, 200, .5f, 0);
		outlineModel.GetComponent<SpriteRenderer> ().sortingLayerName = "TopRhsUI";

		animator = GameObject.Find ("Cockpit").GetComponent<Animator> ();

		decaying = true;
		initd = true;


		flashing = true;
		}




		// Update is called once per frame
		void Update () {
		if (!initd) {
			return;
		}
		if (flashing) {
			if (currFlashSpeed >= maxFlashSpeed || currFlashSpeed <= -maxFlashSpeed) {
				flashIncrement = -flashIncrement;
			}
			currFlashSpeed += flashIncrement;
			mat.color = new Color (flashRvalue, flashGvalue, mat.color.b, mat.color.a+currFlashSpeed);
		}

		if (health >= 99) {
			flashing = true;
			animator.SetInteger ("Power", 2);
		} else {
			flashing = false;
			animator.SetInteger ("Power", 1);
		}
		if (decaying) {
		} else {
			if (health >= 99) {
				model.transform.localScale = new Vector3 (.9f, 1 * .92f);
				animator.SetInteger ("Power", 2);
				health = 100;
			} else if (health >= slowDownThreshold) {
				model.transform.localScale = new Vector3 (.9f, .92f * health / 100f);
				health += Time.deltaTime * repairRate * slowDownModifier;
				animator.SetInteger ("Power", 1);
			} else {
				model.transform.localScale = new Vector3 (.9f, .92f*health/100f);
				health += Time.deltaTime * repairRate;
				animator.SetInteger ("Power", 1);
			}
		}
		if (health >= 99) {
			flashing = true;
			animator.SetInteger ("Power", 2);
		} else {
			flashing = false;
			if (health > 50) {
				mat.color = new Color (0, .75f, 0, initialAlpha);
				animator.SetInteger ("Power", 1);
			} else {
				mat.color = new Color (.75f, 0f, 0, initialAlpha);
				animator.SetInteger ("Power", 1);
			}
		}
	}
	
		// damages the part and returns true if it lowers the part's health to 0
		public void fire() {
				if (health > fireCost) {
						health = health - fireCost;
						model.transform.localScale = new Vector3 (.9f, .92f*health/100f);
						animator.SetInteger ("Power", 1);
				} else {
						health = 0;
						model.transform.localScale = new Vector3 (.9f, 0);
						animator.SetInteger ("Power", 0);
					}
			}
	}