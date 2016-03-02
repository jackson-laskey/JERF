using UnityEngine;
using System.Collections;

public class ButtonClicker : MonoBehaviour {

	public GameController controller;

	private float darkenSprite;

	private ComponentHealth healthBar; 

	private MoveCaptain captain;

	private GameObject model;

	private Material mat;
	private Color upColor;
	private Color downColor;

	// Use this for initialization
	public void init (GameController gContr, float quadR, float quadG, float quadB) {
		controller = gContr;
		healthBar = gameObject.transform.parent.FindChild("Health").GetComponent<ComponentHealth> ();
		captain = GameObject.Find ("Crew").GetComponent<MoveCaptain> ();
		healthBar = gameObject.transform.parent.FindChild ("Health").GetComponent<ComponentHealth> ();

		model = GameObject.CreatePrimitive (PrimitiveType.Quad);
		controller.MakeModel (model, "Button", transform, 0, 0, 1, 1);
		model.name = transform.parent.gameObject.name + "ButtonModel";
		
		mat = model.GetComponent<Renderer> ().material;
		darkenSprite = (.7f);
		upColor = new Color (quadR, quadG, quadB);
		downColor = new Color (quadR*darkenSprite, quadG*darkenSprite, quadB*darkenSprite);
		mat.color = upColor;
	}


	void OnMouseDown() {
		captain.GoToLaserShieldEngine (gameObject.transform.parent.gameObject.tag);
	}

	void PressButton(bool buttonDown) {
		if (buttonDown) {
			mat.color = downColor;
		} else {
			mat.color = upColor;
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
