using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float speed;
    [SerializeField] private Transform bossContainer;
    [SerializeField] private float countDownTeleport;
    [SerializeField] private Transform swordContainer;
    [SerializeField] private float maxBoundY;
    [SerializeField] private Vector2 intitialPostitionBoss;
    [SerializeField] private float delayReturnFalse;
    [SerializeField] private GameObject teleportObject;
    [SerializeField] private AnimationClip teleportClip;
    [SerializeField] private float newCountDown;
    private Coroutine teleportCoroutine;
    private Coroutine returnCoroutine;
    private bool isTeleport;
    private bool isReturn;
    private Vector2 targetPosition;
    private float delayReturnFalseTemp;
    private BossShield bossShield;
    private BossShoot bossShoot;
    private void Start()
    {
        bossShield = gameObject.GetComponent<BossShield>();
        bossShield.onShieldBreak += ModifyCountDown;
        bossShoot = gameObject.GetComponent<BossShoot>();
        bossShoot.onShooting += SetActiveObject;
        targetPosition = Vector2.zero;
        transform.position = intitialPostitionBoss;
        delayReturnFalseTemp = delayReturnFalse;
    }
    private void Update()
    {
        if (gameObject.GetComponent<Health>().GetBossDie()) return;

        if (countDownTeleport > 0)
        {
            countDownTeleport -= Time.deltaTime;
            isTeleport = false;
        }
        else if (swordContainer.childCount == 0)
        {
            isTeleport = true;  
        }
        TargetPositionManager();
        TeleportManager();
        if (isReturn)
        {
            delayReturnFalseTemp -= Time.deltaTime;
        }
        if(delayReturnFalseTemp <= 0)
        {
            isReturn = false;
        }
        if((Vector2)transform.position != intitialPostitionBoss)
        {
            ReturnManager();
        }
    }
    public void SetReturnBoss(bool boolean)
    {
        isReturn = boolean;
        delayReturnFalseTemp = delayReturnFalse;
    }
    private void TeleportManager()
    {
        if(teleportCoroutine == null && isTeleport)
        {
            teleportCoroutine = StartCoroutine(BossTeleport(targetPosition));
        }
        else if(teleportCoroutine != null && !isTeleport) 
        {
            StopCoroutine(teleportCoroutine);
            teleportCoroutine = null;
        }
    }
    private void ReturnManager()
    {
        if (returnCoroutine == null && isReturn)
        {
            returnCoroutine = StartCoroutine(BossTeleport(intitialPostitionBoss));
        }
        else if (returnCoroutine != null && !isReturn)
        {
            StopCoroutine(returnCoroutine);
            returnCoroutine = null;
        }
    }
    private void TargetPositionManager()
    {
        if(targetPosition == Vector2.zero && isTeleport)
        {
            targetPosition = SetTargetPosition();
        }
        else if(targetPosition != Vector2.zero && !isTeleport)
        {
            targetPosition = Vector2.zero;
        }
    }
    public void SetCountDownTeleport(float countDown)
    {
        countDownTeleport = countDown;
    }
    private Vector2 SetTargetPosition()
    {
        return new Vector2(playerTransform.position.x, maxBoundY);
    }
    private void ModifyCountDown()
    {
        countDownTeleport = newCountDown;
    }
    private void SetActiveObject(bool isActive)
    {
        this.enabled = isActive;
    }
    private IEnumerator BossTeleport(Vector2 targetPos)
    {
        bossContainer.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        teleportObject.SetActive(true);
        teleportObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = false; 
        yield return new WaitForSeconds(teleportClip.length);
        transform.position = targetPos;
        teleportObject.SetActive(false);
        teleportObject.GetComponent<Animator>().enabled = false;
        yield return new WaitForEndOfFrame();
        bossContainer.gameObject.SetActive(true);
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
