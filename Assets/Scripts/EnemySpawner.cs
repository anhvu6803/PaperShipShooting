using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private bool isLooping;
    [SerializeField] private Transform player;
    [Header("EnemyShooter")]
    [SerializeField] private List<WaveConfigSO> waveConfigShoot;
    [SerializeField] private float timeBetweenWaveShoot;
    private int currentIndexWayShoot;
    private WaveConfigSO currentWaveShoot;
    private Coroutine shootCoroutine;
    private Coroutine waitShootCoroutine;
    private int countSpawnerShoot;
    private bool isWaitToSpawnShoot;
    private bool isSpawnShoot;
    [Header("EnemyBoom")]
    [SerializeField] private List<WaveConfigSO> waveConfigBoom;
    [SerializeField] private float timeBetweenWaveBoom;
    private WaveConfigSO currentWaveBoom;
    private Coroutine boomCoroutine;
    private bool isSpawnBoom;
    private void Start()
    {
        currentIndexWayShoot = 0;
        countSpawnerShoot = 0;
        StartCoroutine(SpawnEnemyShoot());
    }
    private void Update()
    {
        if(transform.GetChild(0).childCount == 0)
        {
            isSpawnShoot = true;
            countSpawnerShoot++;
        }
        else
        {
            isSpawnShoot = false;
        }
        if (transform.GetChild(1).childCount == 0)
        {
            isSpawnBoom = true;
        }
        if(countSpawnerShoot > 1)
        {
            isWaitToSpawnShoot = true;
        }
        WaitShootManager();
        EnemyBoomSpawn();
    }
    private void EnemyShootSpawn()
    {
        if (isSpawnShoot && shootCoroutine == null)
        {
            shootCoroutine = StartCoroutine(SpawnEnemyShoot());
        }
        else if (!isSpawnShoot && shootCoroutine != null)
        {
            StopCoroutine(SpawnEnemyShoot());
            shootCoroutine = null;
        }  
    }
    private void WaitShootManager()
    {
        if (isWaitToSpawnShoot && waitShootCoroutine == null)
        {
            waitShootCoroutine = StartCoroutine(WaitToSpawnEnemyShooter());
        }
        else if(!isWaitToSpawnShoot && waitShootCoroutine != null)
        {
            StopCoroutine(WaitToSpawnEnemyShooter());
            waitShootCoroutine = null;
        }
    }
    private IEnumerator WaitToSpawnEnemyShooter()
    {
        do
        {
            yield return new WaitForSeconds(timeBetweenWaveShoot);
            EnemyShootSpawn();
        } while (isLooping);
    }
    private void EnemyBoomSpawn()
    {
        if (isSpawnBoom && boomCoroutine == null)
        {
            boomCoroutine = StartCoroutine(SpawnEnemyBoom());
        }
        else if (!isSpawnBoom && boomCoroutine != null)
        {
            StopCoroutine(SpawnEnemyBoom());
            boomCoroutine = null;
        }     
    }
    public WaveConfigSO GetCurrentWaveShoot()
    {
        return currentWaveShoot;
    }
    public WaveConfigSO GetCurrentWaveBoom()
    {
        return currentWaveBoom;
    }
    public int GetCurrentIndexWayShoot()
    {
        return currentIndexWayShoot;
    }
    private IEnumerator SpawnEnemyBoom()
    {
        do
        {
            currentWaveBoom = waveConfigBoom[Random.Range(0, waveConfigBoom.Count)];
            currentWaveBoom.ModifyPathYPos(player.position.y);
            for (int i = 0; i < currentWaveBoom.GetEnemyCount(); i++)
            {
                Instantiate(currentWaveBoom.GetEnemy(i), currentWaveBoom.GetStartPointWave().position, Quaternion.Euler(0, 0, 180), transform.GetChild(1));
                yield return new WaitForSeconds(currentWaveBoom.GetEnemySpawnTime());
            }
            yield return new WaitForSeconds(timeBetweenWaveBoom);
        } while (isLooping);
    }
    private IEnumerator SpawnEnemyShoot()
    {
        currentWaveShoot = waveConfigShoot[Random.Range(0, waveConfigShoot.Count)];
        for (int i = 0; i < currentWaveShoot.GetPathWayCount(); i++)
        {
            currentIndexWayShoot = i;
            Instantiate(currentWaveShoot.GetEnemy(i), currentWaveShoot.GetStartPointWave().position, Quaternion.Euler(0, 0, 180), transform.GetChild(0));
            yield return new WaitForSeconds(currentWaveShoot.GetEnemySpawnTime());
        }
    }
}
