using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreCount : MonoBehaviour {

	private int score;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		score = 0;
		scoreText.text = score.ToString();
	}
	
	public void Increment() {
		score++;
		scoreText.text = score.ToString();
	}
}
