using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private int numberBullet;
    [SerializeField] private float paddingPos;
    [SerializeField] private int maxBullet;
    [SerializeField] private bool isFiring;
    
    [Header("Enemy")]
    [SerializeField] private bool isEnemy;
    [SerializeField] private bool isShooting;
    
    [Header("Player")]
    [SerializeField] private bool isPlayer;
    [SerializeField] private Health health;
    [SerializeField] private GameObject bulletPicker;

    private Camera mainCamera;
    private Vector2 maxBound;
    private Vector2 minBound;
    private Coroutine fireCoroutine;
    private AudioPlayer audioPlayer;
    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    private void Start()
    {
        numberBullet = 1;
        InitBound();
    }
    private void OnEnable()
    {
        fireCoroutine = null;
    }
    private void Update()
    {
        if(numberBullet == maxBullet) GameObject.Destroy(bulletPicker);
        if(isEnemy && IsInBound() && isShooting)
        {
            isFiring = true;
        }
        if (isPlayer && !health.GetPlayerDie())
        {
            isFiring = true;
        }
        else if(isPlayer && health.GetPlayerDie()) 
        {
            isFiring = false;
        }
        Fire();
    }
    public void StopFireCoroutine()
    {
        isFiring = false;
        if (!isFiring && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
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
    public void ModifyNumberBullet(int bonusNumberBullet)
    {
        if(numberBullet < maxBullet)
        {
            numberBullet += bonusNumberBullet;
        }
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
                    Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = transform.up * speed;
                    }
                }
                if (isPlayer)
                {
                    audioPlayer.PlayShootingClip();
                }
                else
                {
                    audioPlayer.PlayEnemyShootingClip();
                }
               
            }
            yield return new WaitForSeconds(fireRate);
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
