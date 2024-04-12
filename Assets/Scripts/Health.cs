using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private int score;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private AnimationClip dieAnimation;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D objectCollider;
    [SerializeField] private bool hasShield;
    [Header("Player")]
    [SerializeField] private bool isPlayer;
    [SerializeField] private bool isApplyCameraShake;
    [SerializeField] private GameObject sonicBoom;
    [SerializeField] private EnemySpawner enemySpawn;
    private ScoreKeeper scoreKeeper;
    private CameraShake shake;
    private StorePower storePower;
    private PowerLevelUp powerLevelUp;
    private bool isPlayerDie;
    private void Awake()
    {
        shake = FindObjectOfType<CameraShake>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        storePower = FindObjectOfType<StorePower>();
        powerLevelUp = FindObjectOfType<PowerLevelUp>();
    }
    private void Start()
    {
        isPlayerDie = false;
        health = maxHealth;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damage = collision.GetComponent<DamageDealer>();
        if(damage != null)
        {
            TakeDamage(damage.GetDamage());
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
        if (!hasShield)
        {
            health -= damage;
            ShakeCamera();
        }
        if(health <= 0)
        {
            Die();
        }
    }
    public void SetHasShield(bool boolean)
    {
        hasShield = boolean;
    }
    private void Die()
    {
        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }
        if (!isPlayer)
        {
            StartCoroutine(WaitForDestroyObject());
            GeneratePower power = FindObjectOfType<GeneratePower>();
            power.PowerGenerate(gameObject.transform.position);
            scoreKeeper.ModifyScore(score);
        }
        else
        {
            isPlayerDie = true;
            if (enemySpawn != null)
            {
                enemySpawn.enabled = false;
            }
            StartCoroutine(WaitForDestroyObject());
        }
    }
    public bool GetPlayerDie()
    {
        return isPlayerDie;
    }
    private IEnumerator WaitForDestroyObject()
    {
        if (animator != null)
        {
            animator.enabled = true;
        }
        float waitingTime = dieAnimation != null ? dieAnimation.length : 0;
        yield return new WaitForSeconds(waitingTime);
        if (isPlayer)
        {
            sonicBoom.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
        
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
