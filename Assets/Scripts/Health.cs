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
    [SerializeField] private ParticleSystem breakEffect;
    [SerializeField] private ParticleSystem dieEffect;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Player")]
    [SerializeField] private bool isPlayer;
    [SerializeField] private bool isApplyCameraShake;
    [SerializeField] private GameObject sonicBoom;
    [SerializeField] private EnemySpawner enemySpawn;
    [SerializeField] private bool isPlayerDie;

    [Header("Boss")]
    [SerializeField] private bool isBoss;
    [SerializeField] private GameObject sonicBoomBoss;

    private bool isBossDie;
    private ScoreKeeper scoreKeeper;
    private CameraShake shake;
    private StorePower storePower;
    private PowerLevelUp powerLevelUp;
    private AudioPlayer audioPlayer;
    private void Awake()
    {
        shake = FindObjectOfType<CameraShake>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        storePower = FindObjectOfType<StorePower>();
        powerLevelUp = FindObjectOfType<PowerLevelUp>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    private void Start()
    {
        isBossDie = false;
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
            audioPlayer.PlayImpactClip();
            TakeDamage(damage.GetDamage());
            PlayEffect(explosionEffect);
            if (!collision.CompareTag("Ultimate") && !collision.CompareTag("Boss"))
            {
                damage.Hit();
            }
        }
        if (collision.CompareTag("SonicBoom") && isPlayer)
        {
            audioPlayer.PlayExplosionClip();
            StartCoroutine(AddForcePlayer());
        }       
        else if (collision.CompareTag("SonicBoom"))
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
        if (!isPlayer && !isBoss)
        {
            StartCoroutine(WaitForDestroyObject());
            GeneratePower power = FindObjectOfType<GeneratePower>();
            power.PowerGenerate(gameObject.transform.position);
            scoreKeeper.ModifyScore(score);
        }
        else if(isPlayer)
        {
            isPlayerDie = true;
            if (enemySpawn != null)
            {
                enemySpawn.enabled = false;
            }
            StartCoroutine(WaitForDestroyObject());
        }
        else if(isBoss)
        {
            isBossDie = true;
            StartCoroutine(WaitForDestroyObject());
            scoreKeeper.ModifyScore(score);
        }
    }
    public bool GetPlayerDie()
    {
        return isPlayerDie;
    }
    public bool GetBossDie()
    {
        return isBossDie;
    }
    private IEnumerator WaitForDestroyObject()
    {
        if (animator != null)
        {
            animator.enabled = true;
            animator.SetBool("isDie", true);
        }
        float waitingTime = dieAnimation != null ? dieAnimation.length + .3f : 0;
        yield return new WaitForSeconds(waitingTime);
        if (isPlayer || isBoss) {
            PlayEffect(dieEffect);
            yield return new WaitForSeconds(dieEffect.main.duration + dieEffect.main.startLifetime.constantMax);
        }
        spriteRenderer.enabled = false;
        PlayEffect(breakEffect);
        if (isPlayer)
        {
            sonicBoom.SetActive(true);
            yield return new WaitForSeconds(sonicBoom.GetComponent<SonicBoom>().GetSonicBoomExistTime());
            Destroy(gameObject);
        }
        else if (isBoss)
        {
            sonicBoomBoss.SetActive(true);
            yield return new WaitForSeconds(sonicBoomBoss.GetComponent<SonicBoom>().GetSonicBoomExistTime());
            Destroy(gameObject);
        }
        else if(health <= 0)
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
    private IEnumerator AddForcePlayer()
    {
        gameObject.GetComponent<Shooter>().enabled = false;
        gameObject.GetComponent<Shooter>().StopFireCoroutine();
        gameObject.GetComponent<Player>().enabled = false;
        yield return new WaitForSeconds(.2f);
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down;
            rb.AddForce(-transform.up * 1000f, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(.8f);
        gameObject.GetComponent<Shooter>().enabled = true;
        gameObject.GetComponent<Player>().enabled = true;
    }
    private void PlayEffect(ParticleSystem effect)
    {
        if(effect != null)
        {
            ParticleSystem instance = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}
