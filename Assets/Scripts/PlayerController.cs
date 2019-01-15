using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject bullet;
	public float moveSpeed = 5;
	public float rotX = 1;
	public float rotY = 1;
	public int shootCD = 20;

	private Vector3 offset;
	private int shootTimer = 0;

	// Use this for initialization
	void Start () 
	{
	
	}

	// Update is called once per frame
	void Update () {
		translate ();
		rotate ();

		shoot ();
	}

	void rotate ()
	{
		float x = rotX * Input.GetAxis("Mouse X");
		//transform.Rotate(new Vector3 (0, x, 0), Space.World);
		float y = rotY * Input.GetAxis("Mouse Y");
		//transform.Rotate(new Vector3 (-1 * y, 0, 0) , Space.Self);
		transform.eulerAngles += new Vector3 (-1 * y, x, 0);

	}

	void translate ()
	{
		Vector3 forw = transform.forward * Input.GetAxis ("Vertical");
		Vector3 lat = transform.right * Input.GetAxis ("Horizontal");
		Vector3 vert = new Vector3(0, 1, 0) * Input.GetAxis("Jump");
		transform.position += Vector3.Normalize(forw + lat + vert) * moveSpeed;
	}

	void shoot ()
	{
		if (shootTimer >= shootCD) {
			if (Input.GetAxis ("Fire1") == 1) {
				Instantiate (bullet, transform.position, transform.rotation);
				shootTimer = 0;
			}
		} else {
			if (shootTimer < shootCD)
			{
				shootTimer++;
			}
		}
	}
}