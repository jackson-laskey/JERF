using UnityEngine;
using System.Collections;

public class ParentEnemy : MonoBehaviour {

	protected int hp;
	protected int fireRate;
	protected int speed;
	protected float cd;
	protected EnemyManager owner;

	protected Collider2D col;
	protected Rigidbody2D body;


	void Fire(float x, float y){ 						//I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
		GameObject bulletObject = new GameObject();			
		Projectile laser = bulletObject.AddComponent<Projectile>();
		laser.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
		laser.init(true);
	}

	//public void init(EnemyManager owner){
	//}

	protected void Move(){
	}


}
