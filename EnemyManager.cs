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
	}

	void SpawnEnemy(string type,float x, float y){
		GameObject enemyObject = new GameObject();
		if (type == "asteroid") {
			Asteroid enemy = enemyObject.AddComponent<Asteroid> ();
			enemy.transform.position = new Vector3(x,y,0);
			enemy.init (this);
			print (y);
		}
//		if (type == "cannonenemy") {
//			enemy = enemyObject.AddComponent<CannonEnemy> ();
//			enemy.transform.position = new Vector3 (x, y, 0);
//			enemy.init (this);
//		}
		print (x);
	}
}
