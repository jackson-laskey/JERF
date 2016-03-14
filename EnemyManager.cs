using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameController owner;
	public bool spawningA;
	private float spawnTime;
	private float time;
	private float freq;

	public void init (GameController g)	{
		owner = g;
		time = 0;
	}

	void Update(){
		if (spawningA && time >= .1) {
			if (Random.value >= (freq / 100f)) {
				SpawnEnemy ("A", Random.Range (-6, -.5f), 6, 0);
			}
			time = 0;
		}
		if (spawnTime < 0){
			spawningA = false;
		}
		if (spawningA) {
			time += Time.deltaTime;
			spawnTime = spawnTime - Time.deltaTime;
		}
	}

	public void getInstruction(string type, int size, int x, float position){
		if (type == "A") {
			spawningA = true;
			freq = size;
			spawnTime = x;
		} else if (type == "L") {
			GameObject lightSpawner = new GameObject ();
			Spawner spawner = lightSpawner.AddComponent<Spawner> ();
			spawner.init (type, size, x, .5f, this, false, false, position);
		} else if (type == "H") {
			GameObject heavySpawner = new GameObject ();
			Spawner spawner = heavySpawner.AddComponent<Spawner> ();
			spawner.init (type, size, x, .5f, this, false, false, position);
		} else if (type == "B") {
			GameObject beamSpawner = new GameObject ();
			Spawner spawner = beamSpawner.AddComponent<Spawner> ();
			spawner.init (type, size, x, .5f, this, false, false, position);
		} else if (type == "S") {
			GameObject sparkSpawner = new GameObject ();
			Spawner spawner = sparkSpawner.AddComponent<Spawner> ();
			spawner.init (type, size, x, .5f, this, false, false, position);
		} else if (type == "P1" || type == "P2" || type == "P3") {
			GameObject powerSpawner = new GameObject ();
			Spawner spawner = powerSpawner.AddComponent<Spawner> ();
			spawner.init (type, size, x, .5f, this, false, false, position);
		}
	}

	public void getFormation(string type, int f){
		if (type == "L") {
			GameObject lightSpawner = new GameObject ();
			Spawner spawner = lightSpawner.AddComponent<Spawner> ();
			spawner.init (type, 1, 0, 0, this, false,true,0);
			spawner.giveFormation (f);
		} else if (type == "A") {
			GameObject asteroidSpawner = new GameObject ();
			Spawner spawner = asteroidSpawner.AddComponent<Spawner> ();
			spawner.init (type, 1, 0, 0, this, false,true,0);
			spawner.giveFormation (f);
		}
	}

	public void SpawnEnemy(string type,float x, float y, float position){
		GameObject enemyObject = new GameObject();
		if (type == "A") {
			Asteroid enemy = enemyObject.AddComponent<Asteroid> ();
			enemy.transform.position = new Vector3(x,y,0);
			enemy.init (this);
		}
		if (type == "H") {
			CannonEnemy enemy = enemyObject.AddComponent<CannonEnemy> ();
			enemy.transform.position = new Vector3 (x, y, 0);
			enemy.init (this, position);
		}
		if (type == "L") {
			SmallEnemy enemy = enemyObject.AddComponent<SmallEnemy> ();
			enemy.transform.position = new Vector3 (x, y, 0);
			enemy.init (this);
		}
		if (type == "B") {
			BeamEnemy enemy = enemyObject.AddComponent<BeamEnemy> ();
			enemy.transform.position = new Vector3 (x, y, 0);
			enemy.init (this);
		}
		if (type == "S") {
			SparkEnemy enemy = enemyObject.AddComponent<SparkEnemy> ();
			enemy.transform.position = new Vector3 (x, y, 0);
			enemy.init (this);
		}
		if (type == "BS") {
			BackgroundStars enemy = enemyObject.AddComponent<BackgroundStars> ();
			enemy.transform.position = new Vector3 (x, y, 0);
		}
		if (type == "P1" || type == "P2" || type == "P3") {
			PowerUp enemy = enemyObject.AddComponent<PowerUp> ();
			enemy.transform.position = new Vector3 (x, y, 0);
			enemy.init (this, type);
		}
	}

	public void SpawnBoss() {
		GameObject boss = new GameObject ();
		boss.AddComponent<Boss> ();
		boss.transform.position = new Vector3 (-3.1f, 6.56f);
		boss.GetComponent<Boss>().init (this);
	}
}
