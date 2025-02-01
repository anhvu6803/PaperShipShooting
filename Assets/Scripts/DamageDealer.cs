using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int initialDamage;
    private AudioPlayer audioPlayer;
    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    public void ResetDamage()
    {
        damage = initialDamage;
    }
    public int GetDamage()
    {
        return damage;
    }
    public void ModifyDamage(int bonusDamage)
    {
        damage += bonusDamage;
    }
    public void Hit()
    {
        Destroy(gameObject);
    }
}
