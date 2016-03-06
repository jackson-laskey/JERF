using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	private string type;
	private int num;
	private float x;
	private float elapsed;
	private float freq;

	private EnemyManager eMan;

	// Use this for initialization
	public void init (string typeName, int quantity, float xLoc, float frequency, EnemyManager parent) {
		type = typeName;
		num = quantity;
		x = xLoc;
		elapsed = 0;
		freq = frequency;
		eMan = parent;
	}
	
	// Update is called once per frame
	void Update () {
		if ((elapsed += Time.deltaTime) >= freq) {
			eMan.SpawnEnemy (type, x, 6);
			num--;
			elapsed = 0;
		}
		if (num == 0) {
			Destroy (this.gameObject);
		}
	}
}
