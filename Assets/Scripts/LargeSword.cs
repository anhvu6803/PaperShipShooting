using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeSword : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip beginStab;
    [SerializeField] private float beginStabDelay;
    [SerializeField] private AnimationClip stab;
    private BoxCollider2D boxCollider;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(Stabing());
    }
    private IEnumerator Stabing()
    {
        animator.enabled = true;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(beginStab.length * beginStabDelay);
        animator.SetBool("isStab", true);
        boxCollider.enabled = true;
        yield return new WaitForSeconds(stab.length);
        animator.SetBool("isEnd", true);
        boxCollider.enabled = false;
    }
}
