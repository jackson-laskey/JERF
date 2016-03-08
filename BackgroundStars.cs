using UnityEngine;
using System.Collections;

public class BackgroundStars : MonoBehaviour {

	private BackgroundStarsModel model;
	private float speed;

	public void Start() {
		this.speed = Random.Range (.1f, .5f);
		transform.eulerAngles = new Vector3(0,0,180);
		var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		model = modelObject.AddComponent<BackgroundStarsModel>();
		gameObject.name = "BackgroundStar";
		model.init(this);
	}

	public void ChangeSpeed(float speed){
		this.speed = speed;
	}

	void Update () {
		if (transform.position.y < -7) {
			Destroy (this.gameObject);
		}
		Move ();
	}

	void Move(){
		transform.Translate (Vector3.up * Time.deltaTime * speed);
	}
}
