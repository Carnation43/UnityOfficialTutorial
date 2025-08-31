using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn setting")]
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    [Header("Enemies spawn setting")]
    public float spawnRange = 9.0f;
    public int enemyCount = 0;
    public int waveNumber = 1;

    [Header("Collected object disappear time setting")]
    public float collectionLifetime = 5.0f;
    public float blinkDuration = 2.0f;
    public float blinkInterval = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy(waveNumber);
        SpawnPowerupObject();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            SpawnPowerupObject();
            waveNumber++;
            SpawnEnemy(waveNumber);
        }
    }

    void SpawnEnemy(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    void SpawnPowerupObject()
    {
        GameObject powerup = Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);

        StartCoroutine(BlinkAndDestory(powerup));
    }

    private Vector3 GenerateSpawnPosition()
    {
        float randomX = Random.Range(-spawnRange, spawnRange);
        float randomZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(randomX, 1, randomZ);

        return randomPos;
    }

    private IEnumerator BlinkAndDestory(GameObject powerup)
    {

        Debug.Log("Start BlinkAndDestory Coroutine");
        float waitTime = collectionLifetime - blinkDuration;

        // 逐帧检查避免在waitTime时间内拾取并销毁物品时获取MeshRender组件
        float elapsed = 0;
        while(elapsed < waitTime)
        {
            if (powerup == null) yield break;
            elapsed += Time.deltaTime;
            yield return null;
        }
        if (powerup == null) yield break;

        MeshRenderer renderer = powerup.GetComponent<MeshRenderer>();
        if(renderer != null)
        {
            Debug.Log("Success Find MeshRenderer");
            float endTime = Time.time + blinkDuration;
            while(Time.time < endTime)
            {
                Debug.Log("Start Blinking");
                // 避免在BlinkDuration时间内拾取物品时执行renderer.enable语句
                if (powerup == null) yield break;
                renderer.enabled = !renderer.enabled;
                yield return new WaitForSeconds(blinkInterval);
            }

            renderer.enabled = false;
        }
        if(powerup != null)
            Destroy(powerup);
    }
}
