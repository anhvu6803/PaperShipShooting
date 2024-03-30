using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private bool isCanFire;
    [SerializeField] private float countDown;
    private float countDownTemp;
    private Coroutine bossFireCoroutine;
    [Header("Boss Animation")]
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private AnimationClip bossFire;
    [SerializeField] private AnimationClip bossEndFire;
    [SerializeField] private AnimationClip bossLockFire;
    [Header("Laser Animation")]
    [SerializeField] private GameObject laserObject;
    [SerializeField] private Animator laserAnimator;
    [SerializeField] private AnimationClip beginFire;
    [SerializeField] private AnimationClip fireLaser;
    [SerializeField] private AnimationClip endFire;
    private BoxCollider2D laserCollider;
    private void Start()
    {
        laserObject.SetActive(false);
        laserAnimator.enabled = false;
        countDownTemp = countDown;
        laserCollider = laserObject.GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if(countDownTemp > 0)
        {
            countDownTemp -= Time.deltaTime;
            isCanFire = false;
        }
        else
        {
            isCanFire = true;
        }
        BossFiringManager();
    }
    private void BossFiringManager()
    {
        if(isCanFire && bossFireCoroutine == null)
        {
            bossFireCoroutine = StartCoroutine(BossFiring());
        }
        else if(!isCanFire && bossFireCoroutine != null)
        {
            StopCoroutine(bossFireCoroutine);
            bossFireCoroutine = null;
        }
    }
    private IEnumerator BossFiring()
    {
        bossAnimator.SetBool("isUseLaser", true);
        yield return new WaitForSeconds(bossFire.length);
        laserObject.SetActive(true);
        laserAnimator.enabled = true;
        laserCollider.enabled = false;
        yield return new WaitForSeconds(beginFire.length);
        laserAnimator.SetBool("isFire", true);
        laserCollider.enabled = true;
        yield return new WaitForSeconds(fireLaser.length);
        laserAnimator.SetBool("isFire", false);
        laserCollider.enabled = false;
        yield return new WaitForSeconds(endFire.length);
        bossAnimator.SetBool("isEndFire", true);
        laserObject.SetActive(false);
        laserAnimator.enabled = false;
        yield return new WaitForSeconds(bossEndFire.length);
        bossAnimator.SetBool("isEndFire", false);
        yield return new WaitForSeconds(bossLockFire.length);
        bossAnimator.SetBool("isUseLaser", false);
        countDownTemp = countDown;
    }
}
