

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Spawner : MonoBehaviour 
{

    public int spawnCount;
    [Range (0,100)]
    public float spawnSize = 1;
    public float minionOffset = 1;
    public GameObject minion;

    void OnEnable () {
//      EventManager.StartListening ("Spawn", spawnListener);
//        EventManager.StartListening ("Spawn", Spawn);
    }

    void OnDisable () {
//      EventManager.StopListening ("Spawn", spawnListener);
//        EventManager.StopListening ("Spawn", Spawn);
    }

    public void Spawn () 
	{
        for (int i = 0; i < spawnCount; i++) 
		{
			Vector3 spawnPosition = transform.TransformPoint (GetSpawnPosition ());

            Quaternion spawnRotation = new Quaternion ();
            spawnRotation.eulerAngles = new Vector3 (0.0f, Random.Range (0.0f, 360.0f));
            if (spawnPosition != Vector3.zero) 
			{
                Instantiate (minion, spawnPosition, spawnRotation);
            }
        }
    }

    Vector3 GetSpawnPosition ()
	{
        Vector3 spawnPosition = new Vector3 ();
        float startTime = Time.realtimeSinceStartup;
        bool test = false;
        while (test == false) 
		{
			Vector2 spawnPositionRaw = Random.insideUnitCircle;
			spawnPosition = new Vector3 (transform.position.x * spawnPositionRaw.y * spawnSize ,
				transform.position.y, transform.position.z * spawnPositionRaw.x * spawnSize);
            test = !Physics.CheckSphere (spawnPosition, 0.75f);
            if (Time.realtimeSinceStartup - startTime > 0.5f)
			{
                Debug.Log ("Time out placing Minion!");
                return Vector3.zero;
            }
        }
        return spawnPosition;
    }
}