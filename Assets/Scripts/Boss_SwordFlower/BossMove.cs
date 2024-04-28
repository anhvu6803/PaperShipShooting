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
    private Rigidbody2D rb;
    private Vector2 minBound;
    private Vector2 maxBound;
    private Coroutine teleportCoroutine;
    private bool isTeleport;
    private BossLaser bossLaser;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bossLaser = gameObject.GetComponent<BossLaser>();
        minBound = gameObject.GetComponent<KeepInScene>().GetMinBound();
        maxBound = gameObject.GetComponent<KeepInScene>().GetMaxBound();
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
        TeleportManager();   
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
    private void TeleportManager()
    {
        if(teleportCoroutine == null && isTeleport)
        {
            teleportCoroutine = StartCoroutine(TeleportToPlayer());
        }
        else if(teleportCoroutine != null && !isTeleport) 
        {
            StopCoroutine(teleportCoroutine);
            teleportCoroutine = null;
        }
    }
    public void SetCountDownTeleport(float countDown)
    {
        countDownTeleport = countDown;
    }
    private Vector2 SetTargetPosition()
    {
        return new Vector2(playerTransform.position.x, Random.Range(minBound.y, maxBound.y));
    }
    private IEnumerator TeleportToPlayer()
    {
        bossContainer.gameObject.SetActive(false);
        transform.position = SetTargetPosition();
        yield return new WaitForSeconds(0.5f);
        bossContainer.gameObject.SetActive(true);
    }
}
