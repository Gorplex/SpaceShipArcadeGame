using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour{
	public GameObject projectile;
	[Tooltip("Player move speed in XYZ.")]
	public float moveSpeed = 5;
	[Tooltip("X sensitivity(mouse).")]
	public float rotX = 4;
	[Tooltip("Y sensitivity(mouse).")]
	public float rotY = 4;
	[Tooltip("Cooldown of launching a projectile in seconds.")]
	public float projectileCD = 1;
	
	private Vector3 offset;
	private float nextProjectile;

	void Start(){
		nextProjectile = Time.time;
	}
	
	void Update() {
		Translate();
		Rotate();
		Shoot();
	}

	void Rotate(){
		float x = rotX * Input.GetAxis("Mouse X");
		//transform.Rotate(new Vector3 (0, x, 0), Space.World);
		float y = rotY * Input.GetAxis("Mouse Y");
		//transform.Rotate(new Vector3 (-1 * y, 0, 0) , Space.Self);
		transform.eulerAngles += new Vector3 (-1 * y, x, 0);

	}

	void Translate(){
		Vector3 forw = transform.forward * Input.GetAxis ("Vertical");
		Vector3 lat = transform.right * Input.GetAxis ("Horizontal");
		Vector3 vert = new Vector3(0, 1, 0) * Input.GetAxis("Jump");
		transform.position += Vector3.Normalize(forw + lat + vert) * moveSpeed*Time.deltaTime;
	}

	void Shoot(){
		if (Time.time >= nextProjectile) {
			if (Input.GetAxis ("Fire1") == 1) {
				Instantiate (projectile, transform.position, transform.rotation);
				nextProjectile = Time.time + projectileCD;
			}
		}
	}
}