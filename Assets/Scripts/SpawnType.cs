using UnityEngine;

[System.Serializable]
public class SpawnType
{
    public GameObject mob;
    [Range(0f,100f)]
    public float parcent;
    public Transform[] positions;

    public Transform GetTransform()
    {
        int index = Random.Range(0, positions.Length);
        return positions[index];
    }
}
