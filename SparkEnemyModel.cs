using UnityEngine;
using System.Collections;

public class SparkEnemyModel : MonoBehaviour {

	private Material mat;
	private SparkEnemy owner;

	public void init(SparkEnemy owner) {
		name = "SparkEnemyModel";
		this.owner = owner;
		transform.parent = owner.transform;	
		transform.localPosition = new Vector3(0,0,0);
		transform.localScale = new Vector3 (.85f, 1, 1f);
		name = "Spark Enemy";
		mat = GetComponent<Renderer>().material;	
		mat.shader = Shader.Find ("Sprites/Default");	// Tell the renderer that our textures have transparency. // Get the material component of this quad object.
		mat.mainTexture = Resources.Load<Texture2D>("Textures/SmallShip");	// Set the texture.  Must be in Resources folder.
		mat.color = new Color(0,1,.5f);											// Set the color (easy way to tint things).
	}

	// Update is called once per frame
	void Update () {

	}
}
