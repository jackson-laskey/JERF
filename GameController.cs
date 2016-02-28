using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	protected EnemyManager eMan;
	protected GameObject ship;
	protected GameObject captain;

	void Start () {
		ship = Instantiate(Resources.Load("Prefabs/Ship") as GameObject);
		captain = Instantiate(Resources.Load("Prefabs/Captain") as GameObject);
		EnemyManager man = new EnemyManager ();
		eMan = gameObject.AddComponent (man);
		eMan.init (this);
	}
		
	void ParseInstruction () {

	}
}

