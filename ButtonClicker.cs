using UnityEngine;
using System.Collections;

public class ButtonClicker : MonoBehaviour {

	public GameController controller;

	private float darkenSprite;

	private ComponentHealth healthBar; 

	private MoveCaptain captain;

	private SpriteRenderer sprite;
	private Color upColor;
	private Color downColor;

	// Use this for initialization
	public void init (GameController gContr, float x, float y, float quadR, float quadG, float quadB) {
		controller = gContr;
		healthBar = gameObject.transform.parent.FindChild("Health").GetComponent<ComponentHealth> ();
		captain = GameObject.Find ("Crew").GetComponent<MoveCaptain> ();
		healthBar = gameObject.transform.parent.FindChild ("Health").GetComponent<ComponentHealth> ();
		controller.MakeSprite (gameObject, "Button", transform.parent, 0, 0, 1, 1, 300);
		gameObject.name = "Button";
		gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "BottomRhsUI";
		gameObject.transform.localPosition = new Vector3 (x, y, 0);
		gameObject.AddComponent<BoxCollider2D> ();
		
		sprite = GetComponent<SpriteRenderer> ();
		darkenSprite = (.7f);
		upColor = new Color (quadR, quadG, quadB);
		downColor = new Color (quadR*darkenSprite, quadG*darkenSprite, quadB*darkenSprite);
		sprite.color = upColor;
	}

	void PressButton(bool buttonDown) {
		if (buttonDown) {
			sprite.color = downColor;
		} else {
			sprite.color = upColor;
		}
		healthBar.decaying = !buttonDown;
	}
		

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name == "Crew") {
			PressButton (true);
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.name == "Crew") {
			PressButton (false);
		}
	}
}
