using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    private float spawningHeight = 3f;
    private float timeToDestroy = 10f;
    private float timeToSpawnNextBuilding = 3f;
    [SerializeField] private GameObject building1;

    void Start()
    {
        StartCoroutine(SpawnBuilding());
    }

    private IEnumerator SpawnBuilding()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, Random.Range(-spawningHeight, spawningHeight), 0);
        GameObject building = Instantiate(building1, spawnPosition, Quaternion.identity);
        Destroy(building, timeToDestroy);

        yield return new WaitForSeconds(timeToSpawnNextBuilding);
        StartCoroutine(SpawnBuilding());
    }


}
