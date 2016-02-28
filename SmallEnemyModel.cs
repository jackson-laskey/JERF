﻿using UnityEngine;
using System.Collections;

public class SmallEnemyModel : MonoBehaviour {

	private Material mat;
	private SmallEnemy owner;

	public void init(SmallEnemy owner) {
		this.owner = owner;
		transform.parent = owner.transform;	
		transform.localPosition = new Vector3(0,0,0);
		name = "Asteroid Model";
		mat = GetComponent<Renderer>().material;	
		mat.shader = Shader.Find ("Sprites/Default");	// Tell the renderer that our textures have transparency. // Get the material component of this quad object.
		mat.mainTexture = Resources.Load<Texture2D>("Textures/asteroid");	// Set the texture.  Must be in Resources folder.
		mat.color = new Color(1,1,1);											// Set the color (easy way to tint things).
	}

	// Update is called once per frame
	void Update () {

	}
}