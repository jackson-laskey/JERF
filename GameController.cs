using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public EnemyManager eMan;
	public GameObject ship;
	public GameObject captain;

	void Start () {
		print ("hey buddy");
		ship = Instantiate(Resources.Load("Prefabs/ShipHandler") as GameObject);
		captain = Instantiate(Resources.Load("Prefabs/Captain") as GameObject);
		print ("well hello");
		eMan = gameObject.AddComponent<EnemyManager>();
		eMan.init (this);
		this.ParseInstruction ();
	}
		
	void ParseInstruction () {
		print ("here");
		eMan.getInstruction("asteroid",5,-4);
	}
}

