using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    private float spawningHeight = 3f;
    private float timeToDestroy = 10f;
    private float timeToSpawnNextBuilding = 3f;
    private float timer;
    private int actualPointsIncreasedSpeed;

    [SerializeField] private List<GameObject> buildings;

    public static BuildingSpawner Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartSpawningBuildings()
    {
        SpawnBuilding();
        actualPointsIncreasedSpeed = 0;
        MoveBuilding.speed = 2;
        timeToSpawnNextBuilding = 3f;
        timer = 0;
    }

    private void Update()
    {
        if (timer > timeToSpawnNextBuilding) 
        { 
            SpawnBuilding();
            timer = 0;
        }

        UpdateBuildingsSpeed();
        timer += Time.deltaTime;
    }

    private void SpawnBuilding()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, Random.Range(-spawningHeight, spawningHeight), 0);

        int randomBuilding = Random.Range(0, buildings.Count);
        GameObject building = Instantiate(buildings[randomBuilding], spawnPosition, Quaternion.identity, transform);
        Destroy(building, timeToDestroy);
    }

    public void DestroyBuildings()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void UpdateBuildingsSpeed()
    {
        int points = GameManager.Instance.points;
        if (points % 10 == 0)
        {
            if (actualPointsIncreasedSpeed != points)
            {
                actualPointsIncreasedSpeed = points;
                MoveBuilding.speed += 0.5f;
                timeToSpawnNextBuilding -= 0.3f;
            }
        }
    }
}
