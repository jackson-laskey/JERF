// Tom Wexler
// Example program to help you get started with your project.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour {

	GameObject asteroidFolder;	// This will be an empty game object used for organizing objects in the Hierarchy pane.
	public List<Asteroid> asteroids;			// This list will hold the gem objects that are created.
	public int num;
	public int frame;
	public float rate;


	// Start is called once when the script is created.
	void Start () {

		asteroidFolder = new GameObject();  
		asteroidFolder.name = "Asteroids";		// The name of a game object is visible in the hHerarchy pane.
		asteroids = new List<Asteroid>();
		frame = 0;
		rate = .98f;

	}

	// Update is called every frame.
	void Update () {
		frame = frame + 1;
		if (frame % 200 == 0) {
			rate = rate - .01f;
		}
		if (Random.value>rate){
			GameObject astObj = new GameObject();			// Create a new empty game object that will hold a gem.
			Asteroid asteroid = astObj.AddComponent<Asteroid>();			// Add the Gem.cs script to the object.
			asteroids.Add(asteroid);
			asteroid.init (this);
		}
	}

}