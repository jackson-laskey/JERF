using UnityEngine;
using System.Collections;

public class CannonEnemy : ParentEnemy {

	private CannonEnemyModel model;

	void Start () {
		hp = 5;
		fireRate = 2;
		speed = 3;
		col = new Collider2D();
		body = new Rigidbody2D();
		transform.eulerAngles = new Vector3(0,0,180);
		var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		model = modelObject.AddComponent<CannonEnemyModel>();	
		model.init(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
