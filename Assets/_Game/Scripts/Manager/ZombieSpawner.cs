using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPosition;
    [SerializeField] private Zombie zombiePrefab;
    public PathCreation.PathCreator[] pathCreatorsList;
    private float zombieCounter;

    public void OnInit()
    {
        CancelInvoke();
        zombieCounter = 0;
        InvokeRepeating(nameof(SpawnZombie), 0, 0.5f);
    }

    void SpawnZombie()
    {
        if (GameManager.instance.IsState(Enums.GameState.Playing) && zombieCounter < LevelController.instance.CurrentLevel.LevelMaxValue)
        {
            zombieCounter++;
            SimplePool.Spawn(zombiePrefab, spawnPosition[0].position, Quaternion.Euler(0, 0, 0)).OnInit();
        }
    }
}



