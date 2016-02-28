using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	void SpawnEnemy(string type,float x, float y){
		GameObject enemyObject = new GameObject();
		ParentEnemy enemy;
		if (type == "asteroid") {
			enemy = enemyObject.AddComponent<Asteroid> ();
			enemy.transform.position = new Vector3(x,y,0);
			enemy.init (this);
		}
		if (type == "cannonenemy") {
			enemy = enemyObject.AddComponent<CannonEnemy> ();
			enemy.transform.position = new Vector3 (x, y, 0);
			enemy.init (this);
		}

	}
}
