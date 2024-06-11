using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private bool isLooping;
    [SerializeField] private Transform player;
    private PowerLevelUp playerLevel;
    [Header("EnemyShooter")]
    [SerializeField] private List<WaveConfigSO> waveConfigShoot;
    [SerializeField] private float initTimeBetweenWaveShoot;
    [SerializeField] private float minTimeWaveShoot;
    [SerializeField] private float timeBetweenWaveShoot;
    private int currentIndexWayShoot;
    private WaveConfigSO currentWaveShoot;
    private Coroutine shootCoroutine;
    private bool isSpawnShoot;
    [Header("EnemyBoom")]
    [SerializeField] private List<WaveConfigSO> waveConfigBoom;
    [SerializeField] private float timeBetweenWaveBoom;
    [SerializeField] private GameObject dangerObject;
    [SerializeField] private float timeAnnouceDangerBeforeSpawnEnemy;
    [SerializeField] private float minTimeWaveBoom;
    private WaveConfigSO currentWaveBoom;
    private Coroutine boomCoroutine;
    private bool isSpawnBoom;
    private void Awake()
    {
        playerLevel = FindObjectOfType<PowerLevelUp>();
    }
    private void OnEnable()
    {
        isLooping = true;
        currentIndexWayShoot = 0;
        timeBetweenWaveShoot = 0;
        shootCoroutine = StartCoroutine(SpawnEnemyShoot());
    }
    private void OnDisable()
    {
        isLooping = false;
        isSpawnShoot = false;
        if (!isSpawnShoot && shootCoroutine != null)
        {
            StopCoroutine(SpawnEnemyShoot());
            shootCoroutine = null;
        }
        isSpawnBoom = false;
        if (!isSpawnBoom && boomCoroutine != null)
        {
            StopCoroutine(SpawnEnemyBoom());
            boomCoroutine = null;
        }
    }
    private void Update()
    {
        EnemyShootSpawn();
        EnemyBoomSpawn();
    }
    private void EnemyShootSpawn()
    {
        if (transform.GetChild(0).childCount == 0)
        {
            isSpawnShoot = true;
            timeBetweenWaveShoot = initTimeBetweenWaveShoot;
        }
        else
        {
            isSpawnShoot = false;
        }
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
    private void EnemyBoomSpawn()
    {
        if (transform.GetChild(1).childCount == 0)
        {
            isSpawnBoom = true;
        }
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
        while (isLooping)
        {
            yield return new WaitForSeconds(timeBetweenWaveBoom - timeAnnouceDangerBeforeSpawnEnemy);
            currentWaveBoom = waveConfigBoom[Random.Range(0, waveConfigBoom.Count)];
            currentWaveBoom.ModifyPathYPos(player.position.y);
            GameObject dangerInstance = Instantiate(dangerObject, currentWaveBoom.GetDangerAppearPoint().position, Quaternion.identity, transform.GetChild(1));
            yield return new WaitForSeconds(timeAnnouceDangerBeforeSpawnEnemy);
            Destroy(dangerInstance);
            for (int i = 0; i < currentWaveBoom.GetEnemyCount(); i++)
            {
                Instantiate(currentWaveBoom.GetEnemy(i), currentWaveBoom.GetStartPointWave().position, Quaternion.Euler(0, 0, 180), transform.GetChild(1));
                yield return new WaitForSeconds(currentWaveBoom.GetEnemySpawnTime());
            }
            timeBetweenWaveBoom = Mathf.Clamp(timeBetweenWaveBoom - Mathf.Clamp((float)playerLevel.GetLevel() / 10, 0, 1), 
                minTimeWaveBoom, float.MaxValue);
        }
    }
    private IEnumerator SpawnEnemyShoot()
    {
        yield return new WaitForSeconds(timeBetweenWaveShoot);
        currentWaveShoot = waveConfigShoot[Random.Range(0, waveConfigShoot.Count)];
        for (int i = 0; i < currentWaveShoot.GetPathWayCount(); i++)
        {
            currentIndexWayShoot = i;
            Instantiate(currentWaveShoot.GetEnemy(i), currentWaveShoot.GetStartPointWave().position, Quaternion.Euler(0, 0, 180), transform.GetChild(0));
            yield return new WaitForSeconds(currentWaveShoot.GetEnemySpawnTime());
        }
        initTimeBetweenWaveShoot = Mathf.Clamp(initTimeBetweenWaveShoot - Mathf.Clamp((float)playerLevel.GetLevel() / 10, 0, 1),
                minTimeWaveShoot, float.MaxValue);
    }
}
