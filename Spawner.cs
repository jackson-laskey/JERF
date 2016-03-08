using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	private string type;
	private int num;
	private float x;
	private float elapsed;
	private float freq;
	private bool background;
	public int formationType;
	private bool formation;
	private float y;

	private EnemyManager eMan;

	// Use this for initialization
	public void init (string typeName, int quantity, float xLoc, float frequency, EnemyManager parent, bool background, bool formation) {
		type = typeName;
		this.background = background;
		num = quantity;
		x = xLoc;
		elapsed = 0;
		freq = frequency;
		eMan = parent;
		this.formation = formation;
		gameObject.name = "Spawner";
		if (background) {
			y = 8;
		} else {
			y = 6;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (formation) {
			if (formationType == 1) {
				LFormation ();
			} else if (formationType == 2) {
				SFormation ();
			}
		} else {
			if ((elapsed += Time.deltaTime) >= freq) {
				eMan.SpawnEnemy (type, x, y);
				num--;
				elapsed = 0;
			}
			if (num == 0 && !background) {
				Destroy (this.gameObject);
			}
		}
	}

	void SFormation(){
		for (int x = -6; x < 0; x++) {
			if (x % 2 == 0) {
				eMan.SpawnEnemy (type, x, 6);
			}
		}
		for (int x = -6; x < 0; x++) {
			if (x % 2 != 0) {
				eMan.SpawnEnemy (type, x, 9);
			}
		}
		for (int x = -6; x < 0; x++) {
			if (x % 2 == 0) {
				eMan.SpawnEnemy (type, x, 12);
			}
		}
		Destroy (this.gameObject);
	}

	void LFormation(){
		for (int x = -6; x < 0; x++) {
			eMan.SpawnEnemy (type, x, 6);
		}
		for (int x = -6; x < 0; x++) {
			eMan.SpawnEnemy (type, x, 9);
		}
		Destroy (this.gameObject);
	}

	public void giveFormation(int formationType){
		this.formationType = formationType;
	}
}
