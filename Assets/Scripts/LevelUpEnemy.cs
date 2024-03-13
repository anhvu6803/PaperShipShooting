using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpEnemy : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private int level;
    [SerializeField] private float surpriseConst;
    private int currentLevel;
    private PowerLevelUp playerLevel;
    [Header("Shooter")]
    [SerializeField] private float speedFire;
    [SerializeField] private float fireRate;
    private Shooter shooter;
    [Header("Health")]
    [SerializeField] private float adjustConstHealth;
    [SerializeField] private float bonusHeatlh;
    [SerializeField] private float bonusMaxHealth;
    private float currentHealth;
    private Health health;
    [Header("Damage")]
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float bonusDamage;
    private DamageDealer enemyDamage;
    private DamageDealer bulletDamage;
    private float currentDamage;
    private void Awake()
    {
        playerLevel = FindObjectOfType<PowerLevelUp>();
    }
    private void Start()
    {
        currentLevel = 1;
        level = playerLevel.GetLevel();
        Debug.Log(level + "LV");
        health = GetComponent<Health>();
        shooter = GetComponent<Shooter>();
        enemyDamage = GetComponent<DamageDealer>();
        if (enemyBullet != null)
        {
            bulletDamage = enemyBullet.GetComponent<DamageDealer>();
        }
        while (currentLevel < level)
        {
            LevelUpHeatlh();
            LevelUpShooter();
            LevelUpEnemyDamage();
            LevelUpBulletDamage();
            currentLevel++;
        }
    }
    private void LevelUpShooter()
    {      
        if(shooter != null)
        {
            shooter.ModifyFireRate(fireRate);
            shooter.ModifySpeed(speedFire);
        }
    }
    private void LevelUpHeatlh()
    {     
        if(health != null)
        {
            adjustConstHealth = (float)currentLevel / 10;
            currentHealth = adjustConstHealth * bonusHeatlh;
            currentHealth = Mathf.Clamp(currentHealth, 0, bonusMaxHealth);
            if (currentLevel % 3 == 0)
            {
                adjustConstHealth = ((float)currentLevel * surpriseConst) / 5 + surpriseConst;
                currentHealth = adjustConstHealth * bonusHeatlh;
            }
            health.ModifyMaxHealth(Mathf.FloorToInt(currentHealth));
        }
    }
    private void LevelUpEnemyDamage()
    {

        if(enemyDamage != null)
        {
            currentDamage = bonusDamage;
            if(currentLevel % 3 == 0)
            {
                currentDamage *= surpriseConst;
            }
            enemyDamage.ModifyDamage(Mathf.FloorToInt(currentDamage));
        }
    }
    private void LevelUpBulletDamage()
    {
        if (bulletDamage != null)
        {
            Debug.Log("LevelUpBulletDamage");
            Debug.Log(bulletDamage.GetDamage() + "dmg");
            currentDamage = bonusDamage;
            if (currentLevel % 3 == 0)
            {
                currentDamage += surpriseConst;
            }
            bulletDamage.ModifyDamage(Mathf.FloorToInt(currentDamage));
        }
    }
}
