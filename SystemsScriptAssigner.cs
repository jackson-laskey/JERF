using UnityEngine;
using System.Collections;

public class SystemsScriptAssigner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject[] systems = new GameObject[3];
		GameObject lasers = GameObject.FindGameObjectsWithTag ("Laser");
		GameObject shields = GameObject.FindGameObjectsWithTag ("Shield");
		GameObject engines = GameObject.FindGameObjectsWithTag ("Engine");
		systems [0] = lasers;
		systems [1] = shields;
		systems [2] = engines;
		foreach (GameObject i in systems) {
			i.transform.FindChild ("Button").gameObject.AddComponent<ButtonClicker> ();
			i.transform.FindChild ("Health").gameObject.AddComponent<ComponentHealth> ();
		}
	}
}
