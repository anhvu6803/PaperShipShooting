using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Music Data", fileName = "New Music Data")]
public class MusicManagerData : ScriptableObject
{
    [SerializeField] private AudioClip openingClip;
    [SerializeField] private AudioClip closingClip;
    [SerializeField] private AudioClip combatClip;
    [SerializeField] private AudioClip bossClip;

    public AudioClip GetOpening()
    {
        return openingClip;
    }
    public AudioClip GetClosing()
    {
        return closingClip;
    }
    public AudioClip GetCombat()
    {
        return combatClip;
    }
    public AudioClip GetBoss()
    {
        return bossClip;
    }
}
