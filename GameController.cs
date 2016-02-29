using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public EnemyManager eMan;
	public GameObject ship;
	public GameObject captain;

	void Start () {
		//ship = Instantiate(Resources.Load("Prefabs/ShipHandler") as GameObject);
		//captain = Instantiate(Resources.Load("Prefabs/Captain") as GameObject);
		ship.SetActive(true);
		captain.SetActive(true);
		eMan = gameObject.AddComponent<EnemyManager>();
		eMan.init (this);
		this.ParseInstruction ();
	}
		
	void ParseInstruction () {
		eMan.getInstruction("cannonenemy",5,-4);
	}
}

