using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameController owner;

	public void init (GameController g)	{
		owner = g;
	}

	public void getInstruction(string type, int size, int x){
		print (type);
		print (size);
		print (x);
		//SpawnEnemy (type,x,5);
		SpawnEnemy (type,x,7);
	}

	void SpawnEnemy(string type,float x, float y){
		GameObject enemyObject = new GameObject();
		if (type == "asteroid") {
			Asteroid enemy = enemyObject.AddComponent<Asteroid> ();
			enemy.transform.position = new Vector3(x,y,0);
			enemy.init (this);
		}
		if (type == "cannonenemy") {
			CannonEnemy enemy = enemyObject.AddComponent<CannonEnemy> ();
			enemy.transform.position = new Vector3 (x, y, 0);
			enemy.init (this);
		}
		if (type == "smallenemy") {
			SmallEnemy enemy = enemyObject.AddComponent<SmallEnemy> ();
			enemy.transform.position = new Vector3 (x, y, 0);
			enemy.init (this);
		}
	}
}
