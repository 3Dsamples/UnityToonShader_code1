using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainSetUp3072 : MonoBehaviour 
{
	public bool start_b = true,end_b = false;
	Terrain terrain;
	TerrainData terrainData;
	TerrainSetUp3072 terrainset;

	// Use this for initialization
	void Start () 
    {
		terrain = GetComponent<Terrain> ();
		terrainset = GetComponent<TerrainSetUp3072>();
		start_b = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(start_b == true && end_b == false)
		{
			terrain.terrainData.size = new Vector3(3072/2,300/2,3072/2);
			terrain.basemapDistance = 128;
			terrain.materialType = Terrain.MaterialType.Custom;
			terrain.reflectionProbeUsage =  UnityEngine.Rendering.ReflectionProbeUsage.Off ;
			terrain.terrainData.thickness = 100;
			terrain.bakeLightProbesForTrees = false;
			terrain.treeDistance = 160;
			terrain.treeBillboardDistance = 80;
			terrain.treeCrossFadeLength = 75;
			terrain.treeMaximumFullLODCount = 70;
			terrain.castShadows = false;
			terrain.terrainData.heightmapResolution = 513;
			terrain.terrainData.SetDetailResolution(2048,16);
			terrain.detailObjectDensity = 0.4f;
			terrain.terrainData.baseMapResolution = 2048;
			start_b = false;
			end_b = true;
			terrainset.enabled = false;
		}
   }

}
