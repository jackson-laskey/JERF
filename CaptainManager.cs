using UnityEngine;
using System.Collections;

public class CaptainManager : MonoBehaviour {

	public GameController controller;

	public GameObject lasers;
	public GameObject engines;
	public GameObject shields;
	public GameObject[] components;
	public GameObject crew;
	public GameObject cockpit;
	public GameObject engineModule;
	public GameObject shieldModule;
	public GameObject hyperShield;
	public GameObject core;



	// Use this for initialization
	public void init (GameController gContr) {
		controller = gContr;
		transform.localScale = new Vector3(1.3f, 1.3f, 1);
		transform.position = new Vector3(3.4f, 0, 0);


		lasers = new GameObject();
		lasers.transform.parent = transform;
		lasers.transform.localPosition = new Vector3 (0, 0, 0);
		lasers.name = "Lasers";
		lasers.transform.localScale = new Vector3 (1, 1, 1);
		engines = new GameObject();
		engines.transform.parent = transform;
		engines.transform.localPosition = new Vector3 (0, 0, 0);
		engines.name = "Engines";
		engines.transform.localScale = new Vector3 (1, 1, 1);
		shields = new GameObject();
		shields.transform.parent = transform;
		shields.transform.localPosition = new Vector3 (0, 0, 0);
		shields.name = "Shields";
		shields.transform.localScale = new Vector3 (1, 1, 1);
		components = new GameObject[3]{ lasers, engines, shields };

		cockpit = new GameObject ();

		controller.MakeSprite (cockpit,"", this.transform, 0, 2.37f, 1, 1, 500);
		cockpit.AddComponent<Animator> ();
		Animator animator = cockpit.GetComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Cockpit_Animation_Controller");
		cockpit.gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "Modules";
		cockpit.name = "Cockpit";

		engineModule = new GameObject ();

		controller.MakeSprite (engineModule,"", this.transform, -1.085f, -1.664f, 1, 1, 500);
		engineModule.AddComponent<Animator> ();
		animator = engineModule.GetComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Engines_Animation_Controller");
		engineModule.gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "Modules";
		engineModule.name = "EngineModule";

		shieldModule = new GameObject ();

		controller.MakeSprite (shieldModule,"", this.transform, 1.085f, -2.15f, 1, 1, 500);
		shieldModule.AddComponent<Animator> ();
		animator = shieldModule.GetComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Shield_Animation_Controller");
		shieldModule.gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "Modules";
		shieldModule.name = "ShieldModule";

		core = new GameObject ();

		controller.MakeSprite (core,"", this.transform,0, -0.38f, 1, 1, 500);
		core.AddComponent<Animator> ();
		animator = core.GetComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Core_Animation_Controller");
		core.gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "Border";
		core.name = "Core";

		hyperShield = new GameObject ();

		controller.MakeSprite (hyperShield,"", this.transform, .225f, 0, 1, 1, 500);
		hyperShield.AddComponent<Animator> ();
		animator = hyperShield.GetComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Hyper_Shield_Animation_Controller");
		hyperShield.gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "Player";
		hyperShield.gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		hyperShield.name = "HyperShield";
		hyperShield.gameObject.GetComponent<SpriteRenderer> ().enabled = false;

		crew = new GameObject ();
		crew.transform.parent = transform;
		crew.AddComponent<MoveCaptain> ();
		controller.MakeSprite (crew, "Button", transform, 0, -1.6f , .5f, .5f, 300);
		crew.name = "Crew";
		crew.GetComponent<MoveCaptain> ().init (controller, components);
	}

	public void Die(){
		GameObject death = new GameObject ();
		controller.MakeSprite (death,"", controller.transform, 0, 0, 1.3f, 1.3f, 500);
		death.transform.localPosition = this.transform.position;
		death.AddComponent<Animator> ();
		Animator animator = death.GetComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Captain_Death_Animation_Controller");
		Destroy (this.gameObject);
	}
}
