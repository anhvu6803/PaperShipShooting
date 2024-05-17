using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitShield : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private PowerShield powerShield;
    [SerializeField] private bool isDontBreak;
    private float existTimeShield;
    private void Start()
    {
        existTimeShield = powerShield.GetExistTime();
    }
    private void Update()
    {
        if (isDontBreak) return;

        existTimeShield -= Time.deltaTime;
        if (existTimeShield <= 0)
        {
            powerShield.TakeDamageShield(powerShield.GetMaxHealth());
            existTimeShield = powerShield.GetExistTime();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damage = collision.GetComponent<DamageDealer>();
        if (damage != null)
        {
            if (!isDontBreak)
            {
                powerShield.TakeDamageShield(damage.GetDamage());
            }
            PlayExplosionEffect(collision.transform.position);
            if (!collision.CompareTag("Ultimate") && !collision.CompareTag("Boss") && !collision.CompareTag("BossShield"))
            {
                damage.Hit();
            }
        }
    }
    private void PlayExplosionEffect(Vector2 colliderPosition)
    {
        if (explosionEffect != null)
        {
            ParticleSystem instance = Instantiate(explosionEffect, colliderPosition, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}
