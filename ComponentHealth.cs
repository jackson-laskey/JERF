using UnityEngine;
using System.Collections;

public class ComponentHealth : MonoBehaviour {

	public float repairRate;
	public float decayModifier;
	public float health;

	public bool decaying;



	// Use this for initialization
	void Start () {
		decaying = true;
		health = 100;
		repairRate = 15f;
		decayModifier = 4.3f;
	}

	// Update is called once per frame
	void Update () {
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
		}
	}

	// damages the part and returns true if it lowers the part's health to 0
	public bool Damage(float damage) {
		float damageModifier = 1;
		if (health == 100) {
			damageModifier = .2f;
		} else if (!decaying) {
			damageModifier = .5f;
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
