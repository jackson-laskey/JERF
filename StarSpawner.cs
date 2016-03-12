using UnityEngine;
using System.Collections;

public class StarSpawner : MonoBehaviour {

	public EnemyManager e;

	public void init (EnemyManager eMan) {
		e = eMan;
	}

	// Update is called once per frame
	void Update () {
		if (Random.value > .95) {
			float x = Random.Range(-6,0) + Random.value;
			float y = 5;
			e.SpawnEnemy("BS", x, y,0);
		}
	}

}
