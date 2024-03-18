using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShield : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    [SerializeField] private Sprite shildSprite;
    [SerializeField] private int maxHealth;
    [SerializeField] private AnimationClip breakShield;
    [SerializeField] private Health playerHealth;
    [SerializeField] private float existTime;
    private int health;
    private StorePower storePower;
    private void Awake()
    {
        storePower = FindObjectOfType<StorePower>();
    }
    public float GetExistTime()
    {
        return existTime;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public void TakeDamageShield(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(BreakShield());
        }
    }
    public void UseShield()
    {
        playerHealth.SetHasShield(true);
        shield.SetActive(true);
        shield.GetComponent<SpriteRenderer>().sprite = shildSprite;
        health = maxHealth;
        storePower.DecreaseShieldCount();
    }
    private IEnumerator BreakShield()
    {
        shield.GetComponent<Animator>().enabled = true;
        playerHealth.SetHasShield(false);
        yield return new WaitForSeconds(breakShield.length);
        shield.SetActive(false);
        shield.GetComponent<Animator>().enabled = false;
    }
}
