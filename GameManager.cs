using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//SpawnEnemy ("asteroid", 0, 6);
		SpawnEnemy ("cannonenemy", 3, 5);
	}
	
	// Update is called once per frame
	void SpawnEnemy(string type,float x, float y){
		GameObject enemyObject = new GameObject();
		ParentEnemy enemy;
		if (type == "asteroid") {
			enemy = enemyObject.AddComponent<Asteroid> ();
			enemy.transform.position = new Vector3(x,y,0);
		}
		if (type == "cannonenemy") {
			enemy = enemyObject.AddComponent<CannonEnemy> ();
			enemy.transform.position = new Vector3(x,y,0);
		}

	}
}
