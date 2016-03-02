using UnityEngine;
using System.Collections;

public class ComponentHealth : MonoBehaviour {

	public GameController controller;

	public float repairRate;
	public float decayModifier;
	public float health;

	public bool decaying;

	public GameObject model;
	public Material mat;
	public Vector3 scale;

	private bool initd;


	// Use this for initialization
	public void init (GameController gCont, float x, float y) {
		controller = gCont;

		transform.position = new Vector3 (x, y, 0);
		model = GameObject.CreatePrimitive (PrimitiveType.Quad);
		controller.MakeModel (model, "Bar", transform, 0, 0, 1, 1);
		mat = model.GetComponent<Renderer> ().material;
		mat.color = new Color (0, .75f, 0);
		scale = model.transform.localScale;

		GameObject outlineModel = GameObject.CreatePrimitive (PrimitiveType.Quad);
		controller.MakeModel (outlineModel, "BarOutline", transform, 0, 0, 1, 1);

		decaying = true;
		health = 100;
		repairRate = 15f;
		decayModifier = 4.3f;

		initd = true;
	}

	// Update is called once per frame
	void Update () {
		if (!initd) {
			return;
		}
		if (decaying) {
			if (health <= 1) {
				scale = new Vector3(1, 0);
				health = 0;
			} else {
				scale = new Vector3 (1, health/100f);
				health -= (Time.deltaTime * repairRate) /decayModifier;
			}
		} else {
			if (health >= 99) {
				scale = new Vector3 (1, 1);
				health = 100;
			} else {
				scale = new Vector3 (1, health/100f);
				health += Time.deltaTime * repairRate;
			}
		}
		if (health > 50) {
			mat.color = new Color((100-health)*.015f, .75f, 0);
		} else {
			mat.color = new Color(.75f, health*.015f, 0);
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
