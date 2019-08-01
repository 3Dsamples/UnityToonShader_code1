using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class ActiveCamFollow : MonoBehaviour 
{
	WarppingSystemScene Warp;
	public GameObject WarpObj;
	public bool ActiveFollowCamera = false;
	public float SpeedOfCameraMovement = 0; 
	float distance = 0;
	float StartTime = 0;
	float distCovered = 0;
	float fracJourney = 0;

	// Use this for initialization
	void Start () 
	{
		
		WarpObj = GameObject.Find ("WarpPoints");
		Warp = WarpObj.GetComponent<WarppingSystemScene> ();
		distance = Vector3.Distance (Warp.CamMovement [0].transform.position
			,Warp.cam.transform.position );
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(ActiveFollowCamera == true)
		{
			Warp.ActiveObjName = transform.root.name;
			Warp.ActiveObjToFollow = GameObject.Find(Warp.ActiveObjName);
			Warp.CamName = "MainCamera"+transform.root.name;
			Warp.cam = GameObject.Find(	Warp.CamName);
			Warp.camMain.depth = 0;
			Warp.audioMain.enabled = false;
			Warp.AudioListenerOnOff (0, true);
			Warp.AudioListenerOnOff (1, false);
			Warp.AudioListenerOnOff (3, false);
			Warp.AudioListenerOnOff (3, false);
			Warp.CameraDepthOverride (0,1);
			Warp.CameraDepthOverride (1,0);
			Warp.CameraDepthOverride (2,0);
			Warp.CameraDepthOverride (3,0);
			StartTime = Time.smoothDeltaTime;
			distCovered = (Time.smoothDeltaTime - StartTime) * SpeedOfCameraMovement;
			fracJourney = distCovered / distance;
			Warp.CamMovement[0].transform.position = Vector3.Lerp(Warp.CamMovement [0].transform.position
				,Warp.cam.transform.position,fracJourney);
//			if(fracJourney == 0)
//			{
//				Warp.CamMovement [0].gameObject.SetActive (false);
//				Warp.cam [0].gameObject.SetActive (true);
//				ActiveFollowCamera = false;
//			}
		}

	}

}
