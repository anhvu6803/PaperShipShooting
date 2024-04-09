using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSword : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float delaySpawnTime;
    [SerializeField] private float waitingTime;
    private float currentAngleY;
    [Header("Large Sword")]
    [SerializeField] private GameObject leftLargeSword;
    [SerializeField] private GameObject rightLargeSword;
    [SerializeField] private GameObject largeSwordSpawn;
    [SerializeField] private float ls_CountDown;
    [SerializeField] private bool isStab;
    private float ls_CountDownTemp;
    private Coroutine largeCoroutine;
    private GameObject currentLargeSwordSpawn;
    [Header("Small Sword")]
    [SerializeField] private GameObject leftSmallSword;
    [SerializeField] private GameObject rightSmallSword;
    [SerializeField] private GameObject smallSwordSpawn;
    [SerializeField] private float ss_CountDown;
    [SerializeField] private bool isSlash;
    private float ss_CountDownTemp;
    private Coroutine smallCoroutine;
    private GameObject currentSmallSwordSpawn;
    private void Start()
    {
        ls_CountDownTemp = ls_CountDown;
        ss_CountDownTemp = ss_CountDown;
    }
    private void Update()
    {
        LargeSwordAction();
        SmallSwordAction();
    }
    private void LargeSwordAction()
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
                currentAngleY = 180;
            }
            else if (randomSpawn < 0.5f || rightLargeSword == null)
            {
                currentLargeSwordSpawn = leftLargeSword;
                currentAngleY = 0;
            }
            largeCoroutine = StartCoroutine(SwordSpawning(currentLargeSwordSpawn, currentAngleY, largeSwordSpawn));
        }
        else if(!isStab && largeCoroutine != null)
        {
            StopCoroutine(largeCoroutine);
            largeCoroutine = null;
        }
    }
    private void SmallSwordAction()
    {
        if (ss_CountDownTemp > 0)
        {
            ss_CountDownTemp -= Time.deltaTime;
            isSlash = false;
        }
        else
        {
            isSlash = true;
        }
        if (rightSmallSword != null || leftSmallSword != null)
        {
            SpawnSmallSwordManager();
        }
    }
    private void SpawnSmallSwordManager()
    {
        if (isSlash && smallCoroutine == null)
        {
            float randomSpawn = Random.value;
            if (randomSpawn > 0.5f || leftSmallSword == null)
            {
                currentSmallSwordSpawn = rightSmallSword;
                currentAngleY = 180;
            }
            else if (randomSpawn < 0.5f || rightSmallSword == null)
            {
                currentSmallSwordSpawn = leftSmallSword;
                currentAngleY = 0;
            }
            smallCoroutine = StartCoroutine(SwordSpawning(currentSmallSwordSpawn, currentAngleY, smallSwordSpawn));
        }
        else if (!isSlash && smallCoroutine != null)
        {
            StopCoroutine(smallCoroutine);
            smallCoroutine = null;
        }
    }
    private IEnumerator SwordSpawning(GameObject sword, float angleY, GameObject swordSpawn)
    {
        sword.SetActive(false);
        yield return new WaitForSeconds(delaySpawnTime);
        Instantiate(swordSpawn, playerTransform.position, Quaternion.Euler(0, angleY, 0));
        yield return new WaitForSeconds(waitingTime);
        sword.SetActive(true);
        if (sword.CompareTag("SmallSword"))
        {
            ss_CountDownTemp = ss_CountDown;
        }
        else if (sword.CompareTag("LargeSword"))
        {
            ls_CountDownTemp = ls_CountDown;
        }
    }
}
