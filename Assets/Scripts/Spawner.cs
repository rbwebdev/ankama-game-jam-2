using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public List<SpawnList> waves;
    public int currentWave = 0;

    private int waveSize = 0;
    private int mobDead = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWave(currentWave);
    }

    // Update is called once per frame
    void Update()
    {
        if (waveSize > 0 && mobDead == waveSize)
        {
            currentWave++;
            if (currentWave < waves.ToArray().Length)
            {
                SpawnWave(currentWave);
            }
            
        }
    }

    private void SpawnWave(int wave)
    {
        waveSize = waves[wave].list.ToArray().Length;
        mobDead = 0;
        foreach (Spawn spawn in waves[wave].list)
        {
            StartCoroutine(SpwanMob(spawn.mob, spawn.position, spawn.delay));
        }
    }

    private IEnumerator SpwanMob(GameObject mob, Transform position, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Instantiate(mob, position.position, position.rotation);
    }

    public void modDead()
    {
        mobDead++;
    }
}
