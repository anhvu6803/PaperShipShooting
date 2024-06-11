using System;
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
    [SerializeField] private Transform bossContainer;
    [SerializeField] private float newCountDown;
    private float countDownTemp;
    private List<Transform> points = new List<Transform>();
    private float angleY;
    private bool isShoot;
    private Coroutine shootCoroutine;
    private BossLaser bossLaser;
    private BossShield bossShield;
    public event Action<bool> onShooting;
    private void Start()
    {
        bossShield = gameObject.GetComponent<BossShield>();
        bossShield.onShieldBreak += ModifyCountDown;
        bossShield._onShieldBreak += SetActiveObject;
        bossLaser = gameObject.GetComponent<BossLaser>();
        foreach (Transform t in pointSpawn.transform)
        {
            points.Add(t);
        }
    }
    private void Update()
    {
        if (gameObject.GetComponent<Health>().GetBossDie()) return;

        if(countDownTemp > 0)
        {
            countDownTemp -= Time.deltaTime;
            isShoot = false;
            onShooting(true);
        }
        else if(!bossLaser.GetLaserFiring())
        {
            isShoot = true;
            onShooting(false);
        }
        SpawnShootManage();
    }
    private void ModifyCountDown()
    {
        countDown = newCountDown;
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
    private void SetActiveObject(bool isActive)
    {
        this.enabled = isActive;
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
            GameObject instance = Instantiate(flowerSword, t.position, Quaternion.Euler(0, angleY, 0), bossContainer);
            instance.SetActive(true);
            yield return new WaitForSeconds(delaySpawnTime);
        }
        countDownTemp = countDown;
    }
}
