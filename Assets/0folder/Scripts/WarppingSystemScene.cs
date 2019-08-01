using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WarppingSystemScene : MonoBehaviour 
{
	
	public GameObject cam;
	public GameObject[] CamMovement;
	public string  CamName;
	public int CamMovementIndex = 0, WarpExitPointIndex = 0,WarpEnterPointIndex = 0;
	public GameObject[] WarpExitPoint,WarpEnterPoint;
	public bool IsInGround = true,IsInSpace = false;
	public int GroundManagerIndex = 0,SpaceManagerIndex = 0;
	public GameObject[] GroundManager;
	public GameObject[] SpaceManager;
	[HideInInspector]
	public Vector3 OrientationFixedPoint = Vector3.zero; 
	const int zeroint = 0;
	public GameObject ActiveObjToFollow;
	public string ActiveObjName;
	string tagName = "ObjToFollow";
	[HideInInspector]
	public Camera[] cameras;
	[HideInInspector]
	public Camera camMain;
	[HideInInspector]
	PlayerOrientation FollowObj;
	[HideInInspector]
	public AudioListener[] audioL;
	[HideInInspector]
	public AudioListener audioMain;

	void Start()
	{
		ActiveObjToFollow = GameObject.Find(ActiveObjName);
		camMain = cam.GetComponent<Camera>();
		FollowObj = ActiveObjToFollow.GetComponent<PlayerOrientation>();
		GroundManager = new GameObject[GroundManagerIndex];
		SpaceManager = new GameObject[SpaceManagerIndex];
		WarpExitPoint = new GameObject[WarpExitPointIndex]; 
		WarpEnterPoint = new GameObject[WarpEnterPointIndex];
		audioL = new AudioListener[CamMovementIndex];
		cameras = new Camera[CamMovementIndex];
		audioMain = cam.GetComponent<AudioListener>();
		OrientationFixedPoint = new Vector3(zeroint, zeroint, zeroint);
		CamMovement[0] = GameObject.Find ("Main Camera_Follow");
		CamMovement[1] = GameObject.Find ("Main Camera_1");
		CamMovement[2] = GameObject.Find ("Main Camera_2");
		CamMovement[3] = GameObject.Find ("Main Camera_3");
		WarpExitPoint[0] = GameObject.Find ("WarppingExitPointNorth");
		WarpExitPoint[1] = GameObject.Find ("WarppingExitPointSouth");
		WarpExitPoint[2] = GameObject.Find ("WarppingExitPointWest");
		WarpExitPoint[3] = GameObject.Find ("WarppingExitPointLest");
		WarpExitPoint[4] = GameObject.Find ("WarppingExitPointSpace");
		WarpEnterPoint[0] = GameObject.Find ("WarppingEnterPointNorth");
		WarpEnterPoint[1] = GameObject.Find ("WarppingEnterPointSouth");
		WarpEnterPoint[2] = GameObject.Find ("WarppinEnterPointWest");
		WarpEnterPoint[3] = GameObject.Find ("WarppingEnterPointLest");
		WarpEnterPoint[4] = GameObject.Find ("WarppingEntePointSpace");
		CamName = "MainCamera"+ActiveObjToFollow.transform.root.name;
		cam = GameObject.Find(CamName);

		for(int i = 0;i <= GroundManager.Length -1; i++)
		{
			GroundManager[i] = GameObject.Find("Ground_" + i);
		}
		for(int i = 0;i <= SpaceManager.Length -1; i++)
		{
			SpaceManager[i] = GameObject.Find("Space_" + i);
		}
		for(int i = 0;i <= cameras.Length -1; i++)
		{
			cameras[i] = CamMovement[i].GetComponent<Camera>();
			audioL[i] = CamMovement[i].GetComponent<AudioListener>();
		}

	}

	void Update()
	{
		
	}

	void OnTriggerEnter(Collider Other)
	{
		if(Other.tag == tagName)
		{
			
		}
	}
	/// <summary>
	/// cameraFollow,Camera1,camera2,camera3.
	/// </summary>
	/// <param name="index">Index.</param>
	/// <param name="enable">If set to <c>true</c> enable.</param>
	public void AudioListenerOnOff ( int index,bool enable)
	{
		audioL[index].enabled = enable;
	}
	/// <summary>
	/// cameraFollow,Camera1,camera2,camera3.
	/// </summary>
	/// <param name="index">Index.</param>
	/// <param name="value">Value.</param>
	public void CameraDepthOverride (int index,float value)
	{
		cameras [index].depth = value;
	}
	/// <summary>
	/// cameraFollow,Camera1,camera2,camera3.
	/// </summary>
	/// <param name="Index">Index.</param>
	/// <param name="value">Value.</param>
	public void FildOfView (int Index,float value)
	{
		cameras [Index].fieldOfView = value;
	}
	/// <summary>
	/// cameraFollow,Camera1,camera2,camera3.
	/// </summary>
	/// <param name="Index">Index.</param>
	/// <param name="value">Value.</param>
	public void FarClipPlane (int Index,float value)
	{
		cameras [Index].farClipPlane = value;
	}
	public void MainCameraforCharacterGenerator(float FildValue,float FarClipPlane,float Depth,
	bool OCC,bool HDr,bool MSAA,bool AllowDynamicResolution )
	{
		camMain.fieldOfView = FildValue;
		camMain.farClipPlane = FarClipPlane;
		camMain.depth = Depth;
		camMain.useOcclusionCulling = OCC;
		camMain.allowHDR = HDr;
		camMain.allowMSAA = MSAA;
		camMain.allowDynamicResolution = AllowDynamicResolution;
	}
	/// <summary>
	/// Follow Object Orientation
	/// </summary>
	public void FollowObjectSetUp()
	{
		if(FollowObj.LocalPos == PlayerOrientation.LocalDirection.NORTH)
		{
			
		}
		else if(FollowObj.LocalPos == PlayerOrientation.LocalDirection.SOUTH)
		{

		}
		else if(FollowObj.LocalPos == PlayerOrientation.LocalDirection.WEST)
		{

		}
		else if(FollowObj.LocalPos == PlayerOrientation.LocalDirection.LEST)
		{

		}
	}

}