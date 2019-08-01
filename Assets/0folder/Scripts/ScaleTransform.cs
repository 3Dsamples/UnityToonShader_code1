using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTransform : MonoBehaviour
{
	
	Vector3 Scale = Vector3.zero;
	public float ScaleX, ScaleY, ScaleZ;
	// Use this for initialization
	void Start () 
	{
		Scale = new Vector3 (ScaleX, ScaleY, ScaleZ);
		transform.localScale = Scale;
	}
	
	// Update is called once per frame
	void Update () 
	{


	}

}
