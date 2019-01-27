using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour{
	
	//for use with events listeners
	public const string enable = "EnablePlayerControll";
	public const string disable = "DisablePlayerControll";
	
    #region PrivateSerializedFields
	#pragma warning disable 0649
    
	[Tooltip("Shows debug raycast(only in Scene view.")]
	[SerializeField] 
    private bool showRaycast = false;
	[Tooltip("Enables rendering of Targeting Cylinder.")]
	[SerializeField] 
    private bool showTargetingCyl = true;
	[Tooltip("LayerMask of enemies to change color on.")]
	[SerializeField]
    private LayerMask layerMask;
	[Tooltip("Targeting cylinder to enable out of fron of player.")]
	[SerializeField]
    private GameObject targetingCyl;
	[Tooltip("Default Color of Targeting Cylinder.")]
	[SerializeField]
    private Color defaultC = Color.white;
	[Tooltip("Color of Targeting Cylinder when over an Enemy.")]
	[SerializeField]
    private Color activeC = Color.magenta;
	
	
	[Tooltip("Player move speed in XYZ.")]
    [SerializeField]
	private float moveSpeed = 5;
	[Tooltip("X sensitivity(mouse).")]
    [SerializeField]
	private float rotX = 4;
	[Tooltip("Y sensitivity(mouse).")]
    [SerializeField]
	private float rotY = 4;
    [Tooltip("X look max angle.")]
    [SerializeField]
	private float maxX = 80;
    [Tooltip("X look min angle.")]
    [SerializeField]
	private float minX = -80;
	[Tooltip("Animator refrence.")]
    [SerializeField]
	private Animator anim;
	
    #pragma warning restore 0649
	#endregion
	
    
	private Renderer targetingCylRend;
	private bool controllsEnabled = true;
	
	void OnEnable(){
		EventManager.StartListening(enable, EnableControlls);
		EventManager.StartListening(disable, DisableControlls);
	}
	void OnDisable(){
		EventManager.StopListening(enable, EnableControlls);
		EventManager.StopListening(disable, DisableControlls);
	}
	private void EnableControlls(){
		controllsEnabled = true;
	}
	private void DisableControlls(){
		controllsEnabled = false;
	}
	
	void Start(){
		if(!targetingCyl){
			targetingCyl = transform.Find("TargetingCylinder").gameObject;
		}
		targetingCylRend = targetingCyl.GetComponent<Renderer>();
		targetingCylRend.material.SetColor("_Color",defaultC);
		targetingCyl.SetActive(showTargetingCyl);
		
		anim = GetComponentInChildren<Animator>();
	}
	
	void Update() {
		if(controllsEnabled){
			Translate();
			Rotate();
			AnimUpdate();
		}
		UpdateTargeting();
	}
	void UpdateTargeting(){
		targetingCyl.SetActive(showTargetingCyl);
		if(showRaycast || showTargetingCyl){
			RaycastUpdate();
		}
	}

	void Rotate(){
		float x = rotX * Input.GetAxis("Mouse X");
		//transform.Rotate(new Vector3 (0, x, 0), Space.World);
		float y = rotY * Input.GetAxis("Mouse Y");
		//transform.Rotate(new Vector3 (-1 * y, 0, 0) , Space.Self);
        //BROKEN
        /*if(transform.eulerAngles.x<=maxX && transform.eulerAngles.x>=minX){
            transform.Rotate(new Vector3 (0, x, 0));
        }*/
        transform.Rotate(new Vector3 (-1 * y, x, 0));
	}

	void Translate(){
		Vector3 forw = transform.forward * Input.GetAxis ("Vertical");
		Vector3 lat = transform.right * Input.GetAxis ("Horizontal");
		Vector3 vert = new Vector3(0, 1, 0) * Input.GetAxis("Jump");
		transform.position += Vector3.Normalize(forw + lat + vert) * moveSpeed*Time.deltaTime;
	}

	void AnimUpdate(){
		if(anim){
			anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
			anim.SetFloat("Vertical", Input.GetAxis("Jump"));
		}
	}
	void RaycastUpdate(){
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
			if(showRaycast){
				Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.magenta);
            }
			//Debug.Log("Did Hit");
			targetingCylRend.material.SetColor("_Color",activeC);
        }
        else
        {
			if(showRaycast){
				Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
			//Debug.Log("Did not Hit");
			targetingCylRend.material.SetColor("_Color",defaultC);
        }
	}
}