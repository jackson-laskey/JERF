using UnityEngine;
using System.Collections;

public class LaserHealth : MonoBehaviour {
	
	public GameController controller;

	public float repairRate;
	public float health;

	public Animator animator;
	
	public GameObject model;
	public Material mat;
	public Vector3 scale;
	Sprite[] stextures;
	
	public bool decaying;
	
	private bool initd;

	private int type;

	private float slowDownThreshold;
	private float slowDownModifier;
	
		// Use this for initialization
	public void init (GameController gCont, float x, float y, int type) {
		stextures = Resources.LoadAll<Sprite> ("Textures/Captain_Effects_Sheet_2");
		this.type = type;
		animator = GameObject.Find ("Cockpit").GetComponent<Animator> ();
		controller = gCont;
		model = new GameObject();
		controller.MakeSprite (model, stextures[6], transform, x, y, 1, .92f, 200, .5f, 0);
//		controller.MakeSprite (model, "Bar", transform, x, y, 1, 1, 200, .5f, 0);
		model.GetComponent<SpriteRenderer> ().sortingLayerName = "BottomRhsUI";
		model.transform.localPosition = new Vector3 (0, -0.623f, 0);
		mat = model.GetComponent<Renderer> ().material;
		mat.color = new Color (0, .75f, 0);
		
		GameObject outlineModel = new GameObject ();
		controller.MakeSprite (outlineModel, stextures[7], transform, 0, 0, 1, 1, 200, .5f, 0);
//		controller.MakeSprite (outlineModel, "BarOutline", transform, 0, -.03f, 1, 1, 200, .5f, 0);
		outlineModel.GetComponent<SpriteRenderer> ().sortingLayerName = "TopRhsUI";
		
		health = 100;
		
		repairRate = 30f;
		decaying = true;
		initd = true;

		slowDownThreshold = 90f;
		slowDownModifier = .4f;
		}
	
		// Update is called once per frame
		void Update () {
		if (!initd) {
			return;
		}
		if (decaying) {
		} else {
			if (health >= 99) {
				model.transform.localScale = new Vector3 (1, 1 * .92f);
				health = 100;
			} else if (health >= slowDownThreshold) {
				model.transform.localScale = new Vector3 (1, .92f * health / 100f);
				health += Time.deltaTime * repairRate * slowDownModifier;
			} else {
				model.transform.localScale = new Vector3 (1, .92f*health/100f);
				health += Time.deltaTime * repairRate;
			}
			if (health > 50) {
				mat.color = new Color((100-health)*.015f, .75f, 0);
			} else {
				mat.color = new Color(.75f, health*.015f, 0);
			}
		}
	}
	
		// damages the part and returns true if it lowers the part's health to 0
		public void fire() {
				if (health > 7) {
						health = health - 7;
						model.transform.localScale = new Vector3 (1, .92f*health/100f);
				} else {
						health = 0;
						model.transform.localScale = new Vector3 (1, 0);
					}
			}
	}