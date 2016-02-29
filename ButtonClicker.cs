using UnityEngine;
using System.Collections;

public class ButtonClicker : MonoBehaviour {

	private float darkenSprite;

	private ComponentHealth healthBar; 

	private MoveCaptain captain;

	private SpriteRenderer buttonSprite;

	private float r;
	private float g;
	private float b;

	// Use this for initialization
	void Start () {
		healthBar = gameObject.transform.parent.FindChild("Health").GetComponent<ComponentHealth> ();
		darkenSprite = (.7f);
		buttonSprite = null;
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
			Color spriteColor = buttonSprite.color;
			r = spriteColor.r;
			g = spriteColor.g;
			b = spriteColor.b;
		} if (healthBar == null) {
			healthBar = gameObject.transform.parent.FindChild ("Health").GetComponent<ComponentHealth> ();
		}
		if (buttonDown) {
			buttonSprite.color = new Color (r*darkenSprite, g * darkenSprite, b * darkenSprite);
		} else {
			buttonSprite.color = new Color (r, g, b);
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
