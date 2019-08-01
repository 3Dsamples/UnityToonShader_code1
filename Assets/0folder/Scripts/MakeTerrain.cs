using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class MakeTerrain : EditorWindow 
{
	GameObject[] terrainX;
	TerrainCollider[] terrainColX; 
	TerrainData[] terrainDbX;
	int TerrainQty = 4;
	public int count1 = 0,count2 = 0,count3 = 0,count4 = 0;
	string ObjName = "";
	GUIStyle guistyle;
	int posxy1 = 255,posxy2 = 255,posxy3 = 255,posxy4 = 255,qty;
	int a,b,c,d;
	bool makeT = false;
	string nameTdb  = "";
	[MenuItem("Tools/MakeTerrain/TerrainSetUp")]

	#if UNITY_EDITOR
	static void Init()
	{
		UnityEditor.EditorWindow window = GetWindow(typeof(MakeTerrain));
		window.position = new Rect(300, 300, 250, 80);
		window.Show();

	}

	void OnInspectorUpdate()
	{
		Repaint();
		TerrainMake();
	}

	void OnGUI()
	{

		nameTdb = EditorGUILayout.DelayedTextField("Terrain Name ","Terrain " + count1.ToString());
		TerrainQty = EditorGUILayout.IntSlider ("Quantity", TerrainQty, 4, 40);
		if (GUI.Button (new Rect (3, 40+15+40, 150, 20), "MakeTerrain")) 
		{
			terrainX = new GameObject[TerrainQty]; makeT = true;
			terrainColX = new TerrainCollider[TerrainQty];
			terrainDbX = new TerrainData[TerrainQty];
			count1 = 0;
		}
		if (GUI.Button (new Rect (3, 40+15+70, 150, 20), "Reset Values")) 
		{
			TerrainQty = 0;
		}

//		terrain = (ParticleSystem)EditorGUI.ObjectField(new Rect(3, 3, position.width - 6, 20), "Choice_Particles", terrain, typeof(ParticleSystem));
//		if (terrain) 
//		{
//			if(count> 10){count = 0;PrefabObj.name = PrefabObj.name+"ChangeName";}
//			includeChildren = GUI.Toggle(new Rect(180+120+70,25,150,20),includeChildren,"IncludeChild");
//			GUI.Toggle(new Rect(160,25,150,20),terrain.isPlaying,"Playing");
//			GUI.Toggle(new Rect(170+60,25,150,20),terrain.isEmitting,"Emitting");
//			GUI.Toggle(new Rect(180+120,25,150,20),terrain.isPaused,"Paused");
//			GUI.Toggle(new Rect(180+120+70,25,150,20),includeChildren,"IncludeChild");
//
//			if (GUI.Button (new Rect (3, 25, 150, 20), "PlayParticle")) 
//			{
//				terrain.Play (includeChildren);
//			} 
//			else if (GUI.Button (new Rect (3, 55, 150, 20), "StopParticles")) 
//			{
//				terrain.Stop (includeChildren, ParticleSystemStopBehavior.StopEmitting);
//			} 
//			else if (GUI.Button (new Rect (3, 55 + 30, 150, 20), "PauseParticles"))
//			{
//				terrain.Pause (includeChildren);
//			} 
//			else if (GUI.Button (new Rect (3, 55 + 30 + 30, 150, 20), "ClearParticles"))
//			{
//				terrain.Stop (includeChildren, ParticleSystemStopBehavior.StopEmittingAndClear);
//			} 
//			else if (GUI.Button (new Rect (3, 55 + 30 + 30 + 30, 150, 20), "CreatePrefab")) 
//			{
//				PrefabObj = GameObject.FindWithTag ("Prefab");
//				PrefabUtility.CreatePrefab ("Assets/Model/ParticleSystems/Prefabs/" + PrefabObj.name+count.ToString() + ".prefab", PrefabObj, new ReplacePrefabOptions ());
//				count ++;
//				Debug.LogAssertion("Count "+count.ToString());
//			} 
//			else if (GUI.Button (new Rect (3, 55 + 30 + 30 + 30 + 30, 150, 20), "ClearConsole"))
//			{
//				Debug.ClearDeveloperConsole();
//			}
//
//		}

	}
	void TerrainMake()
	{
		if(makeT == true)
		{
			if(qty == 0)
			{
				makeT = false;
			}
			for(a = ((terrainX.Length - 1) * 2) / 4 ; a > 0; a--)
			{
				count1 = a;
//				terrainDbX [a].name = nameTdb;
				terrainDbX [a] = new TerrainData ();
				terrainX[a] = Terrain.CreateTerrainGameObject(terrainDbX[a]);
				terrainX [a].transform.root.name = nameTdb;
				terrainX[a].transform.position = new Vector3(0,0,posxy1);
				posxy1 += 255;
			}
			for(b = ((terrainX.Length - 1) * 2)  / 4 ; b > 0; b--)
			{
				count2 = count1+b;
//				terrainDbX [b].name = nameTdb;
				terrainX[b] = Terrain.CreateTerrainGameObject(terrainDbX[b]);
				terrainX [b].transform.root.name = nameTdb;
				terrainX[b].transform.position = new Vector3(255,0,posxy2);
				posxy2 += 255;
			}
			for(a = ((terrainX.Length - 1) * 2) / 4 ; a > 0; a--)
			{
				count3 = count2+c;
//				terrainDbX [c].name = nameTdb;
				terrainX[c] = Terrain.CreateTerrainGameObject(terrainDbX[c]);
				terrainX [c].transform.root.name = nameTdb;
				terrainX[c].transform.position = new Vector3(510,0,posxy3);
				posxy3 += 255;
			}
			for(b = ((terrainX.Length - 1) * 2) / 4 ; b > 0; b--)
			{
				count4 = count3+d;
//				terrainDbX [d].name = nameTdb;
				terrainX[d] = Terrain.CreateTerrainGameObject(terrainDbX[d]);
				terrainX [d].transform.root.name = nameTdb;
				terrainX[d].transform.position = new Vector3(765,0,posxy4);
				posxy4 += 255;
			}
			qty = ((TerrainQty * 2) / 4);qty--;
		}
	}
	#endif
}
