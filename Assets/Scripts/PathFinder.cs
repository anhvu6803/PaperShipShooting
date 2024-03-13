using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeWave { boom, shoot };
public class PathFinder : MonoBehaviour
{
    [SerializeField] private TypeWave typeWave;
    private WaveConfigSO pathWave;
    private List<Transform> wavePoints;
    private EnemySpawner spawner;
    private int pointIndex;
    private Shooter enemyShooter;
    private void Awake()
    {
        spawner = FindObjectOfType<EnemySpawner>();
    }
    void Start()
    {
        pointIndex = 0;
        if(typeWave == TypeWave.shoot)
        {
            InitialShootWave();
        }
        else
        {
            InitialBoomWave();
        }
        transform.position = wavePoints[pointIndex].position;
        enemyShooter = GetComponent<Shooter>();
    }
    private void InitialShootWave()
    {
        pathWave = spawner.GetCurrentWaveShoot();
        wavePoints = pathWave.GetPathWave(spawner.GetCurrentIndexWayShoot());
    }
    private void InitialBoomWave()
    {
        pathWave = spawner.GetCurrentWaveBoom();
        wavePoints = pathWave.GetPathWave(0);
    }
    void Update()
    {
        FollowPath();
    }
    private void FollowPath()
    {
        if(pointIndex < wavePoints.Count)
        {
            Vector3 targetPositon = wavePoints[pointIndex].position;
            float delta = Time.deltaTime * pathWave.GetSpeed();
            transform.position = Vector3.MoveTowards(transform.position, targetPositon, delta);
            if (typeWave == TypeWave.shoot)
            {
                enemyShooter.SetShooting(false);
            }
            if(transform.position == targetPositon) 
            {
                pointIndex++;
            }
        }
        else
        {
            if(typeWave == TypeWave.boom)
            {
                Destroy(gameObject);
            }
            else
            {
                enemyShooter.SetShooting(true);
            }
        }
    }
}
