using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainCopy : MonoBehaviour 
{
	public Terrain CopyedTerrain;
	public string TerrainName = "Terrain_";
	public int NumberOfCopyed = 0;
	[HideInInspector]
	public int num = 0;
	[HideInInspector]
	public Vector3 vec3,vec3p;
	public Vector3 pos;
	[HideInInspector]
	public Vector3 TerrainSize;
	[HideInInspector]
	public Terrain[] t;
	[HideInInspector]
	public int count = 0;
	GameObject[] terrainobj;

	void Start()
	{
		count =	NumberOfCopyed;
	}

	void Update()
	{
		terrainobj = new GameObject[NumberOfCopyed];
		t = new Terrain[NumberOfCopyed];
	}
	// Update is called once per frame
	public void Make() 
	{  

		for(int i = 0;i <= t.Length -1; i++) 
		{   
			t[i] = CopyedTerrain.GetComponent<Terrain>();
			t [i].terrainData = new TerrainData ();
			t [i].terrainData.name =  TerrainName + num.ToString();
			Terrain.CreateTerrainGameObject(t[i].terrainData);
			terrainobj [i] = GameObject.Find ("Terrain");
			terrainobj[i].name = TerrainName+ num.ToString();
			terrainobj [i].transform.parent = transform.root;
			vec3 = new Vector3 (t [i].transform.localPosition.x + pos.x * i, 0, 0);
			terrainobj [i].transform.position = vec3;
			terrainobj [i].AddComponent<TerrainSetUp3072> ();
			terrainobj [i].AddComponent<SetTerrainConnection> ();
			num ++;
		}
	}

}