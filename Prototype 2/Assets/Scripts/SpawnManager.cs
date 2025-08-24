using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;

    // vertical
    private float spawnRangeX = 15;
    private float spawnRangeZ = 20;

    // horizontal
    private float spawnHorizontalX = 35;

    // spawnSetting
    private float spawnInterval = 1.5f;
    private float startDelay = 2;
    private float spawnRate = 0.5f;

    private int orientation;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        
        if(Random.value < spawnRate)
        {
            Debug.Log("Updown");
            Quaternion quaternion = Quaternion.Euler(0, 180, 0);
            Vector3 spawnPosZ = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnRangeZ);
            Instantiate(animalPrefabs[animalIndex], spawnPosZ, quaternion);
        }
        else
        {
            Debug.Log("LeftRight");
            orientation = Random.Range(0, 2);

            Vector3 spawnPosX = new Vector3(orientation == 1 ? spawnHorizontalX : -spawnHorizontalX, 0, Random.Range(-1, 16));

            Quaternion quaternion = Quaternion.Euler(0, (orientation == 1 ? -90 : 90), 0);
            Instantiate(animalPrefabs[animalIndex], spawnPosX, quaternion);
        }
    }

}
