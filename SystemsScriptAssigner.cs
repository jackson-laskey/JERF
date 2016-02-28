using UnityEngine;
using System.Collections;

public class SystemsScriptAssigner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.transform.GetChild (1).gameObject.AddComponent<MoveCaptain> ();
		GameObject[] systems = new GameObject[3];
		GameObject lasers = gameObject.transform.FindChild ("Lasers").gameObject;
		GameObject engines = gameObject.transform.FindChild ("Engines").gameObject;
		GameObject shields = gameObject.transform.FindChild ("Shields").gameObject;
		systems [0] = lasers;
		systems [1] = shields;
		systems [2] = engines;
		foreach (GameObject i in systems) {
			i.transform.FindChild ("Health").gameObject.AddComponent<ComponentHealth> ();
			i.transform.FindChild ("Button").gameObject.AddComponent<ButtonClicker> ();
		}
	}
}
