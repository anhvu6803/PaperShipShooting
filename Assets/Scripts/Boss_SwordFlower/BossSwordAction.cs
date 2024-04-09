using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwordAction : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip beginAction;
    [SerializeField] private AnimationClip endAction;
    [SerializeField] private float beginActionDelay;
    [SerializeField] private AnimationClip action;
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
        yield return new WaitForSeconds(beginAction.length * beginActionDelay);
        animator.SetBool("isAction", true);
        boxCollider.enabled = true;
        yield return new WaitForSeconds(action.length);
        animator.SetBool("isEnd", true);
        boxCollider.enabled = false;
        yield return new WaitForSeconds(endAction.length + 0.2f);
        Destroy(gameObject);
    }
}
