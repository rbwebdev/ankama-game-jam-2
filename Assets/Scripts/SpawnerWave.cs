using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnerWave 
{

    public int mobNumer;
    public float SpawnTime;
    public List<SpawnType> types;
    public string eventName;
    public float WaveDelay = 0f;


    public SpawnType GetSpawnType()
    {
        float rand = Random.Range(0f, 100f);
        float cumulProba = 0f;
        SpawnType typeCurrent = new SpawnType();
        foreach (SpawnType type in types)
        {
            typeCurrent = type;
            if (rand <= type.parcent)
            {
                return typeCurrent;
            }

            cumulProba += type.parcent;
        }

        return typeCurrent;
    }
}
