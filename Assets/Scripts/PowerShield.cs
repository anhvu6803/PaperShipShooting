using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShield : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    [SerializeField] private Sprite shieldSprite;
    [SerializeField] private int maxHealth;
    [SerializeField] private AnimationClip breakShield;
    [SerializeField] private float existTime;
    [SerializeField] private Health healthObject;
    private Animator animator;
    private int health;
    private StorePower storePower;
    private AudioPlayer audioPlayer;
    private void Awake()
    {
        storePower = FindObjectOfType<StorePower>();
        animator = shield.GetComponent<Animator>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
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
        healthObject.SetHasShield(true);
        audioPlayer.PlayShieldClip();
        shield.SetActive(true);
        shield.GetComponent<SpriteRenderer>().sprite = shieldSprite;
        health = maxHealth;
        if (storePower != null)
        {
            storePower.DecreaseShieldCount();
        }
        animator.enabled = true;
    }
    private IEnumerator BreakShield()
    {
        healthObject.SetHasShield(false);
        animator.SetBool("isBreak", true);
        audioPlayer.PlayBreakShieldClip();
        yield return new WaitForSeconds(breakShield.length);
        shield.SetActive(false);
        animator.SetBool("isBreak", false);
        animator.enabled = false;
    }
}
