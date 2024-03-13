using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New WaveConfig")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private List<GameObject> listEnemies;
    [SerializeField] private List<Transform> pathWay;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private float minTimeSpawn;
    [SerializeField] private float spawnTimeVariance;
    [SerializeField] private float speed;
    public int GetEnemyCount()
    {
        return listEnemies.Count;
    }
    public int GetPathWayCount()
    {
        return pathWay.Count;
    }
    public GameObject GetEnemy(int index)
    {
        return listEnemies[index];
    }
    public Transform GetStartPointWave()
    {
        return pathWay[0].GetChild(0);
    }
    public List<Transform> GetPathWave(int index)
    {
        List<Transform> list = new List<Transform>();
        foreach(Transform t in pathWay[index])
        {
            list.Add(t);
        } 
        return list;
    }
    public void ModifyPathYPos(float newPosY)
    {
        pathWay[0].position = new Vector2(pathWay[0].position.x, newPosY);
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetEnemySpawnTime()
    {
        float randomTime = Random.Range(timeBetweenSpawn - spawnTimeVariance, timeBetweenSpawn + spawnTimeVariance);
        return Mathf.Clamp(randomTime, minTimeSpawn, float.MaxValue);
    }

}
