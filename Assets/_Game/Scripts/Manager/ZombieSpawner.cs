using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waveList;
    
    public void OnInit()
    {
        for (int i = 0; i < waveList.Length; i++)
        {
            StartCoroutine(SpawnWave(waveList[i], waveList[i].StartTime));
        }
    }

    IEnumerator SpawnWave(Wave wave, float spawnTime)
    {
        yield return new WaitUntil(() => GameManager.instance.IsState(Enums.GameState.Playing));    // Wait until game state is Playing
        yield return new WaitForSeconds(spawnTime);
        float cnt = 0;
        while (cnt < wave.Amount)
        {
            for (int j = 0; j < wave.SpawnPosition.Length; j++)
            {
                cnt++;
                Zombie zb = SimplePool.Spawn<Zombie>(wave.ZombiePrefab, wave.SpawnPosition[j].position, Quaternion.Euler(0, 0, 0));
                zb.Path = wave.Path;
                zb.OnInit();
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(0.2f);
        }
            
        
    }
}

[System.Serializable]
public class Wave
{
    public Zombie ZombiePrefab;
    public float Amount;
    public float StartTime;
    public Transform[] SpawnPosition;
    public Transform[] Path;
}



