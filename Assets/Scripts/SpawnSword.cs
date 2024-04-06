using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSword : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform playerTransform;
    [Header("Large Sword")]
    [SerializeField] private GameObject leftLargeSword;
    [SerializeField] private GameObject rightLargeSword;
    [SerializeField] private GameObject largeSwordSpawn;
    [SerializeField] private float ls_CountDown;
    [SerializeField] private float ls_DelaySpawnTime;
    [SerializeField] private float ls_WaitingTime;
    [SerializeField] private bool isStab;
    private float ls_CountDownTemp;
    private Coroutine largeCoroutine;
    private GameObject currentLargeSwordSpawn;
    private float currenAngleY;
    private void Start()
    {
        ls_CountDownTemp = ls_CountDown;
    }
    private void Update()
    {
        if (ls_CountDownTemp > 0)
        {
            ls_CountDownTemp -= Time.deltaTime;
            isStab = false;
        }
        else
        {
            isStab = true;
        }
        if (rightLargeSword != null || leftLargeSword != null)
        {
            SpawnLargeSwordManager();
        }
    }
    private void SpawnLargeSwordManager()
    {
        if(isStab && largeCoroutine == null)
        {
            float randomSpawn = Random.value;
            if(randomSpawn > 0.5f || leftLargeSword == null)
            {
                currentLargeSwordSpawn = rightLargeSword;
                currenAngleY = 180;
            }
            else if (randomSpawn < 0.5f || rightLargeSword == null)
            {
                currentLargeSwordSpawn = leftLargeSword;
                currenAngleY = 0;
            }
            largeCoroutine = StartCoroutine(SpawnLargeSword(currentLargeSwordSpawn, currenAngleY));
        }
        else if(!isStab && largeCoroutine != null)
        {
            StopCoroutine(largeCoroutine);
            largeCoroutine = null;
        }
    }
    private IEnumerator SpawnLargeSword(GameObject largeSword, float angleY)
    {
        largeSword.SetActive(false);
        yield return new WaitForSeconds(ls_DelaySpawnTime);
        GameObject instance = Instantiate(largeSwordSpawn, playerTransform.position, Quaternion.Euler(0, angleY, 0));
        yield return new WaitForSeconds(ls_WaitingTime);
        Destroy(instance);
        largeSword.SetActive(true);
        ls_CountDownTemp = ls_CountDown;
    }
}
