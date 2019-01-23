using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Realtime;
using Photon.Pun;

//public class NetworkManager : MonoBehaviourPunCallbacks {
public class NetworkManager : MonoBehaviourPunCallbacks {
    
	#region PrivateSerializedFields
	#pragma warning disable 0649
	
	[Tooltip("The UI Text to inform the user about the connection progress.")]
	[SerializeField]
	private GameObject deadCamera;
	[Tooltip("Refence to crosshairs for enabling.")]
    [SerializeField] 
	private GameObject crosshairs;
	[Tooltip("The UI Text to inform the user about the connection progress.")]
	[SerializeField]
	private Text feedbackText;
	[Tooltip("The maximum number of players per room.")]
	[SerializeField]
	private byte maxPlayersPerRoom = 4;
	[Tooltip("The prefab to use for representing the player.")]
	[SerializeField]
	private GameObject playerPrefab;
	[Tooltip("This client's version number. Users are separated from each other by gameVersion.")]
	[SerializeField]
	private string gameVersion = "0.1";
	
	#pragma warning restore 0649
	#endregion
	
	bool intentToConnect = true;
	void Awake(){
		// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
		PhotonNetwork.AutomaticallySyncScene = true;
	}
	void Start(){
		Connect();
    }
	void ClearFeedback(){
		if (feedbackText)
			feedbackText.text = "";
	}
	void LogFeedback(string message){
		if (feedbackText)
			feedbackText.text += "\n"+message;
	}
	
	public static void CheckedSetActive(GameObject obj, bool active, string name){
		if(obj){
			obj.SetActive(active);
		}else{
			Debug.Log("<Color=Red><b>Missing</b></Color> " + name + " refrence in NetworkManager.cs atached to GameManager GameObject");
		}
	}
	
	//#region PhotonExampleCode 
	//Code region from "PUN Basic tutorial" to connect, and join/create room automatically
	
	
	/// <summary>
	/// Start the connection process. 
	/// - If already connected, we attempt joining a random room
	/// - if not yet connected, Connect this application instance to Photon Cloud Network
	/// </summary>
	public void Connect(){
		ClearFeedback();
		// keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
		intentToConnect = true;

		// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
		if (PhotonNetwork.IsConnected)
		{
			LogFeedback("Joining Room...");
			// #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
			PhotonNetwork.JoinRandomRoom();
		}else{
			LogFeedback("Connecting...");
			// #Critical, we must first and foremost connect to Photon Online Server.
			PhotonNetwork.GameVersion = this.gameVersion;
			PhotonNetwork.ConnectUsingSettings();
		}
	}
	
	#region MonoBehaviourPunCallbacks CallBacks
	// below, we implement some callbacks of PUN
	// you can find PUN's callbacks in the class MonoBehaviourPunCallbacks


	/// <summary>
	/// Called after the connection to the master is established and authenticated
	/// </summary>
	public override void OnConnectedToMaster()
	{
		// we don't want to do anything if we are not attempting to join a room. 
		// this case where intentToConnect is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
		// we don't want to do anything.
		if (intentToConnect)
		{
			LogFeedback("OnConnectedToMaster: Next -> try to Join Random Room");
			Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");
	
			// #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
			PhotonNetwork.JoinRandomRoom();
		}
	}
	
	/// <summary>
	/// Called when a JoinRandom() call failed. The parameter provides ErrorCode and message.
	/// </summary>
	/// <remarks>
	/// Most likely all rooms are full or no rooms are available. <br/>
	/// </remarks>
	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		LogFeedback("<Color=Red>OnJoinRandomFailed</Color>: Next -> Create a new Room");
		Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

		// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
		PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom});
	}
	
	/// <summary>
	/// Called after disconnecting from the Photon server.
	/// </summary>
	public override void OnDisconnected(DisconnectCause cause)
	{
		LogFeedback("<Color=Red>OnDisconnected</Color> "+cause);
		Debug.LogError("PUN Basics Tutorial/Launcher:Disconnected");
		
		// #Critical: we failed to connect or got disconnected. There is not much we can do. Typically, a UI system should be in place to let the user attemp to connect again.

		intentToConnect = false;
		CheckedSetActive(crosshairs, false, "crosshairs");
		CheckedSetActive(deadCamera, true, "deadCamera");
	}
	
	/// <summary>
	/// Called when entering a room (by creating or joining it). Called on all clients (including the Master Client).
	/// </summary>
	/// <remarks>
	/// This method is commonly used to instantiate player characters.
	/// If a match has to be started "actively", you can call an [PunRPC](@ref PhotonView.RPC) triggered by a user's button-press or a timer.
	///
	/// When this is called, you can usually already access the existing players in the room via PhotonNetwork.PlayerList.
	/// Also, all custom properties should be already available as Room.customProperties. Check Room..PlayerCount to find out if
	/// enough players are in the room to start playing.
	/// </remarks>
	public override void OnJoinedRoom()
	{
		LogFeedback("<Color=Green>OnJoinedRoom</Color> with "+PhotonNetwork.CurrentRoom.PlayerCount+" Player(s)");
		Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running.");
	
		// #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.AutomaticallySyncScene to sync our instance scene.
		if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
		{
			Debug.Log("We load the Scene(disabled)");
			// #Critical
			// Load the Room Level. 
			//Disabled Relaing Starting scene
			//PhotonNetwork.LoadLevel("Main");
		}
		StartSpawnProcess(0);
	}

	#endregion
	//#endregion //of PhotonExampleCode
	void StartSpawnProcess(float respawnTime){
		CheckedSetActive(crosshairs, false, "crosshairs");
		CheckedSetActive(deadCamera, true, "deadCamera");
		StartCoroutine("SpawnPlayer", respawnTime);
	}
	IEnumerator SpawnPlayer(float respawnTime){
		yield return new WaitForSeconds(respawnTime);
		
		if(playerPrefab){
			//Instantiate(this.playerPrefab, new Vector3(0f,0f,0f), Quaternion.identity);
			PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity);
		}else{
			Debug.Log("<Color=Red><b>Missing</b></Color> playerPrefab from NetworkManager.cs atached to GameManager GameObject");
		}
		CheckedSetActive(crosshairs, true, "crosshairs");
		CheckedSetActive(deadCamera, false, "deadCamera");
	}
	
}
