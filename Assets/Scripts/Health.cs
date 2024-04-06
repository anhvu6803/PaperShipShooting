using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private int score;
    [Header("Player")]
    [SerializeField] private bool isPlayer;
    [SerializeField] private bool hasShield;
    [SerializeField] private bool isApplyCameraShake;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private AnimationClip dieAnimation;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private GameObject sonicBoom;
    [SerializeField] private EnemySpawner enemySpawn;
    [SerializeField] private CircleCollider2D playerCollider;
    private ScoreKeeper scoreKeeper;
    private CameraShake shake;
    private StorePower storePower;
    private PowerShield powerShield;
    private PowerLevelUp powerLevelUp;
    private float existTimeShield;
    private bool isPlayerDie;
    private void Awake()
    {
        shake = FindObjectOfType<CameraShake>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        storePower = FindObjectOfType<StorePower>();
        powerShield = FindObjectOfType<PowerShield>();
        powerLevelUp = FindObjectOfType<PowerLevelUp>();
    }
    private void Start()
    {
        isPlayerDie = false;
        health = maxHealth;
        existTimeShield = powerShield.GetExistTime();
    }
    private void Update()
    {
        if (hasShield)
        {
            existTimeShield -= Time.deltaTime;
            if(existTimeShield <= 0)
            {
                powerShield.TakeDamageShield(powerShield.GetMaxHealth());
                existTimeShield = powerShield.GetExistTime();                
            }
        }
    }
    public float GetHealth()
    {
        return health;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public void ModifyMaxHealth(int bonusHealth)
    {
        maxHealth += bonusHealth;
        health += bonusHealth;
    }
    public void ModifyHealth(int recovery)
    {
        health += recovery;
    }
    public void SetHasShield(bool boolean)
    {
        hasShield = boolean;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damage = collision.GetComponent<DamageDealer>();
        if(damage != null)
        {
            TakeDamage(damage.GetDamage());
            ShakeCamera();
            PlayExplosionEffect();
            if (!collision.CompareTag("Ultimate") && !collision.CompareTag("Boss"))
            {
                damage.Hit();
            }
        }
        if(collision.CompareTag("SonicBoom"))
        {
            Destroy(gameObject);
        }
        if (isPlayer && collision.CompareTag("Power"))
        {
            CollectPower power = collision.GetComponent<CollectPower>();
            if(power.GetPowerType() == PowerType.Shield)
            {
                storePower.IncreaseShieldCount();
            }
            else if (power.GetPowerType() == PowerType.Ultimate)
            {
                storePower.IncreaseUltimateCount();
            }
            else
            {
                powerLevelUp.CollectExp(40);
            }
            Destroy(collision.gameObject);
        }
    }
    private void TakeDamage(int damage)
    {
        if (hasShield)
        {
            powerShield.TakeDamageShield(damage);
        }
        else
        {
            health -= damage;
        }
        if(health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        if(!isPlayer)
        {
            Destroy(gameObject);
            GeneratePower power = FindObjectOfType<GeneratePower>();
            power.PowerGenerate(gameObject.transform.position);
            scoreKeeper.ModifyScore(score);
        }
        else
        {
            isPlayerDie = true;
            enemySpawn.enabled = false;
            playerCollider.enabled = false;
            StartCoroutine(WaitForDestroyPlayer());
        }
    }
    public bool GetPlayerDie()
    {
        return isPlayerDie;
    }
    private IEnumerator WaitForDestroyPlayer()
    {
        playerSprite.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(dieAnimation.length);
        sonicBoom.SetActive(true);
        Destroy(gameObject);
    }
    private void ShakeCamera()
    {
        if(shake != null && isApplyCameraShake)
        {
            shake.Play();
        }
    }
    private void PlayExplosionEffect()
    {
        if(explosionEffect != null)
        {
            ParticleSystem instance = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}
