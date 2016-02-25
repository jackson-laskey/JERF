using UnityEngine;
using System.Collections;

public class ComponentHealth : MonoBehaviour {

	private float repairRate;
	private float decayRate;
	private float thresholds;

	public bool decaying;

	public float health;
	public float numThresholds;
	public float level;


	// Use this for initialization
	void Start () {
		decayRate = 4.3f;
		decaying = true;
		health = 100;
		repairRate = .2f;
	}

	// Update is called once per frame
	void Update () {
		if (decaying) {
			if (health <= repairRate/2) {
				gameObject.transform.localScale = new Vector3(1, health);
				health = 0;
			} else {
				gameObject.transform.localScale = new Vector3 (1, health/100f);
				health -= repairRate/decayRate;
			}
		} else {
			if (health >= 100-repairRate/(2*decayRate)) {
				gameObject.transform.localScale = new Vector3 (1, health/100f);
				health = 100;
			} else {
				gameObject.transform.localScale = new Vector3 (1, health/100f);
				health += repairRate;
			}
		}
		/*level = Mathf.RoundToInt(  ((health*numThresholds)-((health*numThresholds)%100))/100)+1;
		if (health == 0) {
			level = 0;
		}*/
		print (health);
		if (health > 50) {
			gameObject.GetComponent<SpriteRenderer>().color = new Color((100-health)*.015f, .75f, 0);
		} else {
			gameObject.GetComponent<SpriteRenderer>().color = new Color(.75f, health*.015f, 0);
		}

	}
}
