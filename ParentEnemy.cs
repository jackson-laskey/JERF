using UnityEngine;
using System.Collections;

public class ParentEnemy : MonoBehaviour {

	protected int hp;
	protected int fireRate;
	protected int speed;
	protected EnemyManager owner;

	protected Collider2D col;
	protected Rigidbody2D body;

	public void init(EnemyManager owner){
	}

	protected void Move(){
	}


}
