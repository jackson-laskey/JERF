﻿using UnityEngine;
using System.Collections;

public class LaserHealth : MonoBehaviour {
	
	public GameController controller;

	public float repairRate;
	public float health;
	
	public GameObject model;
	public Material mat;
	public Vector3 scale;
	Sprite[] stextures;
	
	public bool decaying;
	
	private bool initd;

	private int type;
	private Animator animator;
	private float slowDownThreshold;
	private float slowDownModifier;

	private bool flashing;
	private float flashClock;
	private float flashInterval;
	
		// Use this for initialization
	public void init (GameController gCont, float x, float y, int type) {
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
		
		GameObject outlineModel = new GameObject ();
		controller.MakeSprite (outlineModel, stextures[7], transform, 0, 0, 1, 1, 200, .5f, 0);
//		controller.MakeSprite (outlineModel, "BarOutline", transform, 0, -.03f, 1, 1, 200, .5f, 0);
		outlineModel.GetComponent<SpriteRenderer> ().sortingLayerName = "TopRhsUI";

		animator = GameObject.Find ("Cockpit").GetComponent<Animator> ();

		health = 100;
		
		repairRate = 30f;
		decaying = true;
		initd = true;

		slowDownThreshold = 85f;
		slowDownModifier = .4f;

		flashing = true;
		flashClock = 0;
		flashInterval = .2f;
		}




		// Update is called once per frame
		void Update () {
		if (!initd) {
			return;
		}
		if (flashing) {
			flashClock += Time.deltaTime;
			if (flashClock > flashInterval) {
				mat.color = new Color (1, .5f, 0);
				flashClock = 0;
			} else if (flashClock > (2 * flashInterval) / 3) {
				mat.color = new Color (1, 0f, 0);
			} else if (flashClock > flashInterval / 3) {
				mat.color = new Color (1, .8f, 0);
			} else {
				mat.color = new Color (1, .5f, 0);
			}
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
				mat.color = new Color (0, .75f, 0);
				animator.SetInteger ("Power", 1);
			} else {
				mat.color = new Color (.75f, 0f, 0);
				animator.SetInteger ("Power", 1);
			}
		}
	}
	
		// damages the part and returns true if it lowers the part's health to 0
		public void fire() {
				if (health > 7) {
						health = health - 7;
						model.transform.localScale = new Vector3 (.9f, .92f*health/100f);
						animator.SetInteger ("Power", 1);
				} else {
						health = 0;
						model.transform.localScale = new Vector3 (.9f, 0);
						animator.SetInteger ("Power", 0);
					}
			}
	}