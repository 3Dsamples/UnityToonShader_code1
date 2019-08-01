using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class PlayerOrientation : MonoBehaviour
{
	[HideInInspector]
	public Vector3 OrientationFixedPoint = Vector3.zero;
	Vector3 Vecx,Vecy,Vecz;
//	[Header("SidePosition")]
	float ForwardOrBackward_dot,LeftOrRight_dot,UpOrDown_dot;
	public enum LocalDirection{NORTH = 0,SOUTH = 1,LEST = 2,WEST = 3}
	[Header("Orientation")]
	public LocalDirection LocalPos = LocalDirection.NORTH;
	const int zeroint = 0;
//	[Header("Distance values")]
	[HideInInspector]
	public float ActualDistance = 0,TotalDistance = 0,DistanceX,DistanceY,DistanceZ;
//	[Header("Relative Distance")]
	Vector3 RelativeDistance;
	Rigidbody rb;
	[Header("Relative Distance")]
	public Vector2 Vec2XZ,Vec2YZ,Vec2YX;
	float WestValueX,WestValueY;

	// Use this for initialization
	void Start () 
	{
		OrientationFixedPoint = new Vector3 (zeroint, zeroint, zeroint);
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		Vecx = new Vector3(transform.position.x,0,0);
		Vecy = new Vector3(0,transform.position.y,0);
		Vecz = new Vector3(0,0,transform.position.z);
		Vec2XZ = new Vector2(transform.position.x,transform.position.z);
		Vec2YZ = new Vector2(transform.position.y,transform.position.z);
		Vec2YX = new Vector2(transform.position.y,transform.position.x);
		WestValueX = Vec2XZ.x; WestValueX = WestValueX * -1;
		WestValueY = Vec2XZ.y; WestValueY = WestValueY * -1;
		//relatives Direction
		Vector3 ForwardOrBackward = transform.TransformDirection(transform.forward);
		Vector3 LeftOrRight = transform.TransformDirection(transform.right);
		Vector3 UpOrDown = transform.TransformDirection(transform.up);
		RelativeDistance  = OrientationFixedPoint - transform.position;
		//relative Vector
//		Vec2XZ = transform.TransformVector (Vec2XZ);
//		Vec2YZ = transform.TransformVector (Vec2YZ);
//		Vec2YX = transform.TransformVector (Vec2YX);
		//Distance in Positive values
		DistanceX = Vector3.Distance (Vecx,OrientationFixedPoint);
		DistanceY = Vector3.Distance (Vecy,OrientationFixedPoint);
		DistanceZ = Vector3.Distance (Vecz,OrientationFixedPoint);
		//Position
		ForwardOrBackward_dot = Vector3.Dot(RelativeDistance, ForwardOrBackward);
		LeftOrRight_dot = Vector3.Dot(RelativeDistance, LeftOrRight);
		UpOrDown_dot = Vector3.Dot(RelativeDistance, UpOrDown);

		//North
		if ((Vec2XZ.x > 0 && Vec2XZ.y > 0))
		{
			if(Vec2XZ.x == Vec2XZ.y)
			{	
			    LocalPos = LocalDirection.NORTH;		
			}
			else if(Vec2XZ.x < Vec2XZ.y)
			{
			    LocalPos = LocalDirection.NORTH;
			}
			//Lest
			else if(Vec2XZ.x > 0 && Vec2XZ.x > Vec2XZ.y) 
			{
				LocalPos = LocalDirection.LEST;
			}
		} 
		//West
		else if((Vec2XZ.x < 0 && WestValueX > Vec2XZ.y)) 
		{
		   	LocalPos = LocalDirection.WEST;
		}
		else if((Vec2XZ.x < 0 && WestValueX < Vec2XZ.y)) 
		{	
			LocalPos = LocalDirection.NORTH;		
		}
//		//South
		if ((Vec2XZ.x < 0 && Vec2XZ.y < 0))
		{
			if(Vec2XZ.x == Vec2XZ.y)
			{	
				LocalPos = LocalDirection.SOUTH;		
			}
			else if(Vec2XZ.x < 0 && WestValueX < WestValueY)
			{
				LocalPos = LocalDirection.SOUTH;
			}
			//West
			else if((Vec2XZ.x < 0 && WestValueX > WestValueY)) 
			{
				LocalPos = LocalDirection.WEST;
			}
		} 
		if(Vec2XZ.x > 0 && Vec2XZ.y < 0) 
		{
			if(Vec2XZ.x > 0 && Vec2XZ.x > WestValueY) 
			{
				LocalPos = LocalDirection.LEST;
			}
			else if((Vec2XZ.x > 0 && WestValueX < WestValueY)) 
			{	
				LocalPos = LocalDirection.SOUTH;		
			}
		}
	}

}
