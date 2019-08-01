using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainCopy))]
public class SimpleMakeTerrain : Editor
{

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();
		TerrainCopy copy = (TerrainCopy)target;
		if(GUILayout.Button("MakeTerrain"))
		{
			copy.Make ();

	     }
		if(GUILayout.Button("Reset Name and number"))
		{
			copy.TerrainName ="Terrain_";
			copy.num = 0;
		}
	}
}
