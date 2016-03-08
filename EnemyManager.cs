﻿using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameController owner;
	private bool spawningA;
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
				SpawnEnemy ("A", Random.Range (-6, -.5f), 6);
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

	public void getInstruction(string type, int size, int x){
		if (type == "A") {
			spawningA = true;
			freq = size;
			spawnTime = x;
		} else if (type == "L") {
			GameObject lightSpawner = new GameObject ();
			Spawner spawner = lightSpawner.AddComponent<Spawner> ();
			spawner.init (type, size, x, .5f, this, false,false);
		} else if (type == "H") {
			GameObject heavySpawner = new GameObject ();
			Spawner spawner = heavySpawner.AddComponent<Spawner> ();
			spawner.init (type, size, x, .5f, this, false,false);
		}
		else if (type == "B") {
			GameObject beamSpawner = new GameObject ();
			Spawner spawner = beamSpawner.AddComponent<Spawner> ();
			spawner.init (type, size, x, .5f, this, false,false);
		}
		else if (type == "S") {
			GameObject sparkSpawner = new GameObject ();
			Spawner spawner = sparkSpawner.AddComponent<Spawner> ();
			spawner.init (type, size, x, .5f, this, false,false);
		}
	}

	public void getFormation(string type, int f){
		if (type == "L") {
			GameObject lightSpawner = new GameObject ();
			Spawner spawner = lightSpawner.AddComponent<Spawner> ();
			spawner.init (type, 1, 0, 0, this, false,true);
			spawner.giveFormation (f);
		} else if (type == "A") {
			GameObject asteroidSpawner = new GameObject ();
			Spawner spawner = asteroidSpawner.AddComponent<Spawner> ();
			spawner.init (type, 1, 0, 0, this, false,true);
			spawner.giveFormation (f);
		}
	}

	public void SpawnEnemy(string type,float x, float y){
		GameObject enemyObject = new GameObject();
		if (type == "A") {
			Asteroid enemy = enemyObject.AddComponent<Asteroid> ();
			enemy.transform.position = new Vector3(x,y,0);
			enemy.init (this);
		}
		if (type == "H") {
			CannonEnemy enemy = enemyObject.AddComponent<CannonEnemy> ();
			enemy.transform.position = new Vector3 (x, y, 0);
			enemy.init (this);
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
	}
}
