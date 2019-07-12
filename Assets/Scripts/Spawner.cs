using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public List<SpawnerWave> waves;
    public int currentWave = 0;

    private int waveSize = 0;
    private int mobDead = 0;
    
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

    private void SpawnWave(int waveNumber)
    {
        SpawnerWave wave = waves[waveNumber];
        waveSize = wave.mobNumer;
        mobDead = 0;
        if (wave.eventName != null)
        {
            IEvent ev = (IEvent)gameObject.GetComponent(wave.eventName);
            if (ev != null)
            {
                ev.Trigger();
            } 
        } 
        if (waveSize > 0)
        {
            StartCoroutine(spawnWaveMobs(wave, wave.WaveDelay));
        }
    }

    private IEnumerator spawnWaveMobs(SpawnerWave wave, float delayTime)
    {
        Debug.Log("ici");
        yield return new WaitForSeconds(delayTime);
        float maxTimeInterval = wave.SpawnTime / waveSize;
        float delay = 0f;
        for (int i = 0; i < waveSize; i++)
        {
            delay += Random.Range(0f, maxTimeInterval);
            SpawnType type = wave.GetSpawnType();
            StartCoroutine(SpwanMob(type.mob, type.GetTransform(), delay));
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
