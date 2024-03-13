using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private int numberBullet;
    [SerializeField] private float paddingPos;
    [SerializeField] private int maxBullet;
    [Header("Enemy")]
    [SerializeField] private bool isEnemy;
    [SerializeField] private bool isFiring;
    [SerializeField] private bool isShooting;
    private Coroutine fireCoroutine;
    private Camera mainCamera;
    private Vector2 maxBound;
    private Vector2 minBound;
    [Header("Player")]
    [SerializeField] private bool isPlayer;
    private void Start()
    {
        numberBullet = 1;
        InitBound();
    }
    private void Update()
    {
        if(isEnemy && IsInBound() && isShooting)
        {
            isFiring = true;
        }
        if (isPlayer)
        {
            isFiring = true;
        }
        Fire();
    }
    public void SetShooting(bool isActive)
    {
        isShooting = isActive;
    }
    private void InitBound()
    {
        mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
    private bool IsInBound()
    {
        return (transform.position.x > minBound.x && transform.position.x < maxBound.x)
            && (transform.position.y > minBound.y && transform.position.y < maxBound.y);
    }
    private void Fire()
    {
        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireNormalBullet());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }
    public void ModifySpeed(float bonusSpeed)
    {
        speed -= bonusSpeed;
    }
    public void ModifyFireRate(float bonusFireRate)
    {
        fireRate -= bonusFireRate;
    }
    private IEnumerator FireNormalBullet()
    {
        while(true)
        {
            for (int i = 0; i < numberBullet; i++)
            {
                if (isEnemy && isShooting || isPlayer)
                {
                    GameObject instance = Instantiate(bullet, RandomPositionBullet(i), Quaternion.identity);
                    instance.SetActive(true);
                    Debug.Log(instance.GetComponent<DamageDealer>().GetDamage());
                    Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = transform.up * speed;
                    }
                }
            }
            yield return new WaitForSeconds(fireRate);
        }
    }
    public void ModifyNumberBullet(int bonusBullet)
    {
        if (numberBullet < maxBullet)
        {
            numberBullet += bonusBullet;
        }
    }
    private Vector2 RandomPositionBullet(int index)
    {
        List<Vector2> list = new List<Vector2>();
        list.Add(transform.position + new Vector3(0, paddingPos, 0));
        list.Add(new Vector2(transform.position.x - paddingPos, transform.position.y));
        list.Add(new Vector2(transform.position.x + paddingPos, transform.position.y));  
        return list[index];
    }
}
