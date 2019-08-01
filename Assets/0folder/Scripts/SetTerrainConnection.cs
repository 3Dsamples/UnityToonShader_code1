using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetTerrainConnection : MonoBehaviour 
{
	[Header("Terrain Left,Terrain Top,Terrain Right,Terrain Botton")]
	public Terrain[] terrainObj;
	Terrain TerrainConection;
	SetTerrainConnection conect;
	public bool enableConnection = false;
	// Use this for initialization
	void Start () 
	{
		conect = GetComponent<SetTerrainConnection> ();
		terrainObj = GetComponents<Terrain> ();	
		terrainObj = new Terrain[4]; 

	}
	#if UNITY_EDITOR_64
	// Update is called once per frame
	void Update ()
	{
		if(enableConnection == true)
		{
			conect.enabled = true;
		}
		else
		{
			conect.enabled = false;
		}
		TerrainConection.SetNeighbors(terrainObj[0],terrainObj[1],terrainObj[2],terrainObj[3]);
	}
	#endif
}
