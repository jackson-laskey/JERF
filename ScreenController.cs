using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScreenController : MonoBehaviour {
	public AudioClip startSounds;
	public AudioSource audio;

	public EnemyManager eMan;
	public GameObject instructions;
	public Button startButton;
	public Button helpButton;
	public Button quitGame;
	GameObject name;
	Sprite[] title;
	private float count;
	private int spt;
	SpriteRenderer rend;
	// Use this for initialization
	void Start () {
		instructions.SetActive (false);
		title = Resources.LoadAll<Sprite> ("Textures/Title_Sheet");
		name = new GameObject ();
		name.transform.position = new Vector3 (0, 2, 0);
		name.transform.localScale = new Vector2 (1.5f, 1.5f);
		spt = 0;
		name.AddComponent<SpriteRenderer> ();
		rend = name.GetComponent<SpriteRenderer> ();
		rend.sprite = title [spt];
		count = .75f;

		startSounds = Resources.Load ("Sounds/startSounds") as AudioClip;

		audio = gameObject.AddComponent<AudioSource> ();
		audio.loop = true;
		audio.clip = startSounds;
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject Starspawner = new GameObject ();
		StartScreenStarSpawner spawner = Starspawner.AddComponent<StartScreenStarSpawner> ();
		spawner.init (eMan);
		if (count <= 0) {
			if (spt == 0) {
				spt = 1;
			} else {
				spt = 0;
			}
			count = .75f;
		} else {
			count = count - Time.deltaTime;
		}
		rend.sprite = title [spt];
	}

	/*void OnGUI(){
		if (GUI.Button(new Rect(Screen.width/2 - 40, Screen.height/2 -40, 100,50), "Start Game")){
			Application.LoadLevel ("Mechanic Study");
		}
		if (GUI.Button (new Rect (Screen.width/2 - 40, Screen.height/2+15, 100, 50), "Instructions")) {

		}
		if (GUI.Button (new Rect (Screen.width/2 -40, Screen.height/2 + 70, 100, 25), "Quit")) {
			Application.Quit();
		}
	}*/

	public void StartGame(){
		Application.LoadLevel ("Mechanic Study");
	}

	public void Help(){
		instructions.SetActive (true);
		startButton.gameObject.SetActive (false);
		helpButton.gameObject.SetActive (false);
		quitGame.gameObject.SetActive (false);
	}

	public void Back(){
		startButton.gameObject.SetActive (true);
		helpButton.gameObject.SetActive (true);
		quitGame.gameObject.SetActive (true);
		instructions.SetActive (false);
	}

	public void Quit(){
		Application.Quit ();
	}
}
