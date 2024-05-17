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
    private Rigidbody2D rb;
    private Coroutine teleportCoroutine;
    private Coroutine returnCoroutine;
    private bool isTeleport;
    private BossLaser bossLaser;
    [SerializeField] private bool isReturn;
    private Vector2 targetPosition;
    private float delayReturnFalseTemp;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bossLaser = gameObject.GetComponent<BossLaser>();
        targetPosition = Vector2.zero;
        transform.position = intitialPostitionBoss;
        delayReturnFalseTemp = delayReturnFalse;
    }
    private void Update()
    {
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
    private void FixedUpdate()
    {
        if (bossLaser.GetLaserFiring())
        {
            Vector2 direction = new Vector2(playerTransform.position.x - transform.position.x, 0).normalized;
            rb.velocity = direction * Time.deltaTime * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
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
    private IEnumerator BossTeleport(Vector2 targetPos)
    {
        bossContainer.gameObject.SetActive(false);
        transform.position = targetPos;
        yield return new WaitForSeconds(0.5f);
        bossContainer.gameObject.SetActive(true);
    }
}
