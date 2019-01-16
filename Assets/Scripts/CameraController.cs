using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player;
    public float speed;
    public float JumpSpeed;
    public float rotationSpeed;

	private Vector3 offset;
    private Rigidbody playerRB;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
        playerRB = player.GetComponent<Rigidbody>();
    }
    
	void FixedUpdate ()
	{
		float moveHorizontal = 0;
		float moveVertical = Input.GetAxis ("Vertical");
        float moveJump = Input.GetAxis ("Jump");

        
        if(Input.GetKey("q") && !Input.GetKey("e")){
            moveHorizontal = -1f;
        }
        if(!Input.GetKey("q") && Input.GetKey("e")){
            moveHorizontal = 1f;
        }

		Vector3 movement = new Vector3 (moveHorizontal * speed, moveJump * JumpSpeed, moveVertical * speed);

		playerRB.AddForce (Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) *movement);
	}
	// Update is called once per frame
	void LateUpdate () {
        float turn = rotationSpeed * Input.GetAxis ("Horizontal");

        offset = Quaternion.AngleAxis(turn, Vector3.up) * offset;
        
        transform.Rotate(0, turn, 0, Space.World);
		transform.position = player.transform.position + offset;
	}
}
