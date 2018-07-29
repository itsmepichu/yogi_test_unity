using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class world : MonoBehaviour {

    public float 
        worldSpeed = 3.5f,
        obstacleSpawnInterval = 3.0f;

    public GameObject 
        playerPrefab,
        terrainPrefab;

    public GameObject[] obstaclePrefabs;

    private float
        startXPosTerrain = -9.5f,
        startYPosTerrain = -3.5f,
        startXPosPlayer = -6.55f,
        startYPosPlayer = -1.0f,
        newStartXPosTerrain = 18.5f;

    private int objectPoolSize = 15;
    private GameObject[] terrainArray;

    // Use this for initialization
    void Start () {
        terrainArray = new GameObject[objectPoolSize];
    
        // Instantiating World Objects
        for(int i=0; i<objectPoolSize; ++i)
        {
            terrainArray[i] = Instantiate(terrainPrefab, new Vector3(startXPosTerrain, startYPosTerrain, 0.0f), Quaternion.identity);
            startXPosTerrain += 2.0f;
        }
        Instantiate(playerPrefab, new Vector3(startXPosPlayer, startYPosPlayer, 0.0f), Quaternion.identity);
        StartCoroutine(SpawnObstacles());
	}
	
	// Update is called once per frame
	void Update () {

        // Optimized Object Pool Logic
		for(int i=0; i<objectPoolSize; ++i)
        {
            terrainArray[i].transform.Translate(Vector3.left * worldSpeed * Time.deltaTime);
            if(terrainArray[i].transform.position.x < -11.5f)
            {
                terrainArray[i].transform.position = new Vector3(newStartXPosTerrain, startYPosTerrain, 0.0f);
            }
        }

	}

    private IEnumerator SpawnObstacles()
    {
        while(true)
        {
            Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], new Vector3(10.0f, -2.2f), Quaternion.identity);
            yield return new WaitForSeconds(obstacleSpawnInterval);
        }
    }

}
