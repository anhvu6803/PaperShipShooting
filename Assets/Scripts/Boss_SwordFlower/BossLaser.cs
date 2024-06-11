using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private bool isCanFire;
    [SerializeField] private float countDown;
    [SerializeField] private Transform swordContainer;
    [SerializeField] private GameObject bossContainer;
    [SerializeField] private float newCountDown;
    private BossMove bossMove;
    [SerializeField] private float countDownTemp;
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
    private BossShield bossShield;
    private BossShoot bossShoot;
    private void Start()
    {
        bossShield = gameObject.GetComponent<BossShield>();
        bossShield.onShieldBreak += ModifyCountDown;
        bossShoot = gameObject.GetComponent<BossShoot>();
        bossShoot.onShooting += SetActiveObject;
        laserObject.SetActive(false);
        laserAnimator.enabled = false;
        countDownTemp = countDown;
        laserCollider = laserObject.GetComponent<BoxCollider2D>();
        bossMove = gameObject.GetComponent<BossMove>();
    }
    private void ModifyCountDown()
    {
        countDown = newCountDown;
    }
    private void Update()
    {
        if (gameObject.GetComponent<Health>().GetBossDie()) return;

        if (countDownTemp > 0)
        {
            countDownTemp -= Time.deltaTime;
            isCanFire = false;
        }
        else if (swordContainer.childCount == 0)
        {
            isCanFire = true;
        }
        if (bossContainer.activeSelf)
        {
            BossFiringManager();
        }
    }
    public bool GetLaserFiring()
    {
        return isCanFire;
    }
    private void SetActiveObject(bool isActive)
    {
        this.enabled = isActive;
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
        laserObject.transform.localScale = new Vector3(1, (transform.position.y - 0.35f + 5) / 1.3f, 1);
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
        bossMove.SetCountDownTeleport(countDown);
        bossMove.SetReturnBoss(true);
    }
}