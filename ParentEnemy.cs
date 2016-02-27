using UnityEngine;
using System.Collections;

public class ParentEnemy : MonoBehaviour {

	protected int hp;
	protected int fireRate;
	protected int speed;

	protected Collider2D col;
	protected Rigidbody2D body;
	protected Projectile bullet = new Projectile();

	void Fire(float x, float y){ 						//I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
		GameObject bulletObject = new GameObject();			
		bullet laser = bulletObject.AddComponent<bullet>();
		laser.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
		laser.init();
	}

	void Move(){
	}
}
