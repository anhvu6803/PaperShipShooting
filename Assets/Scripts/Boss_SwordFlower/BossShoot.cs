using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [SerializeField] private GameObject flowerBloom;
    [SerializeField] private GameObject pointSpawn;
    [SerializeField] private GameObject flowerSword;
    [SerializeField] private float delaySpawnTime;
    [SerializeField] private float countDown;
    private float countDownTemp;
    private List<Transform> points = new List<Transform>();
    private float angleY;
    private bool isShoot;
    private Coroutine shootCoroutine;
    private void Start()
    {
        foreach (Transform t in pointSpawn.transform)
        {
            points.Add(t);
        }
    }
    private void Update()
    {
        if(countDownTemp > 0)
        {
            countDownTemp -= Time.deltaTime;
            isShoot = false;
        }
        else
        {
            isShoot = true;
        }
        SpawnShootManage();
    }
    private void SpawnShootManage()
    {
        if(shootCoroutine == null && isShoot) 
        {
            shootCoroutine = StartCoroutine(SpawnShoot());
        }
        else if(shootCoroutine != null && !isShoot)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
    }
    private IEnumerator SpawnShoot()
    {
        PowerShield powerShield = flowerBloom.GetComponent<PowerShield>();
        powerShield.UseShield();
        yield return new WaitForSeconds(powerShield.GetExistTime());
        foreach (Transform t in points)
        {
            if (t.position.x > 0)
            {
                angleY = 180;
            }
            else
            {
                angleY = 0;
            }
            GameObject instance = Instantiate(flowerSword, t.position, Quaternion.Euler(0, angleY, 0));
            instance.SetActive(true);
            yield return new WaitForSeconds(delaySpawnTime);
        }
        countDownTemp = countDown;
    }
}
