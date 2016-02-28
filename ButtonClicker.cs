using UnityEngine;
using System.Collections;

public class ButtonClicker : MonoBehaviour {

	private float darkenSprite;

	private ComponentHealth healthBar; 

	private MoveCaptain captain;

	private SpriteRenderer buttonSprite;

	// Use this for initialization
	void Start () {
		healthBar = gameObject.transform.parent.FindChild("Health").GetComponent<ComponentHealth> ();
		darkenSprite = (.7f);
		buttonSprite = gameObject.GetComponent<SpriteRenderer> ();
		captain = GameObject.Find ("Crew").GetComponent<MoveCaptain> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		captain.GoToLaserShieldEngine (gameObject.transform.parent.gameObject.tag);
	}

	void PressButton(bool buttonDown) {
		if (buttonSprite == null) {
			buttonSprite = gameObject.GetComponent<SpriteRenderer> ();
		}if (healthBar == null) {
			healthBar = gameObject.transform.parent.FindChild ("Health").GetComponent<ComponentHealth> ();
		}
		Color spriteColor = buttonSprite.color;
		if (buttonDown) {
			buttonSprite.color = new Color (spriteColor.r * darkenSprite,
				spriteColor.g * darkenSprite,
				spriteColor.b * darkenSprite);
		} else {
			buttonSprite.color = new Color (spriteColor.r * (1/darkenSprite),
				spriteColor.g * (1/darkenSprite),
				spriteColor.b * (1/darkenSprite));
		}
		healthBar.decaying = !buttonDown;
	}
		
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "PlayerController") {
			PressButton (true);
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag == "PlayerController") {
			PressButton (false);
		}
	}
}
