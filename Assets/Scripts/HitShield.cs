using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitShield : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private PowerShield powerShield;
    private float existTimeShield;
    private void Start()
    {
        existTimeShield = powerShield.GetExistTime();
    }
    private void Update()
    {
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
            powerShield.TakeDamageShield(damage.GetDamage());
            PlayExplosionEffect();
            if (!collision.CompareTag("Ultimate") && !collision.CompareTag("Boss"))
            {
                damage.Hit();
            }
        }
    }
    private void PlayExplosionEffect()
    {
        if (explosionEffect != null)
        {
            ParticleSystem instance = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}
