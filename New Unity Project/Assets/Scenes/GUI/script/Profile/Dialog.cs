using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    private Animator animator;
    private AnimationClip AttackedAnimationClip;
    void Start()
    {
        animator = GetComponent<Animator>();        
    }

    public void btnAdd_Click()
    {
        animator.SetBool("open", true);
    }

    public void btnClose_Click()
    {
        animator.SetBool("open", false);
        StartCoroutine(setMuti());
    }

    IEnumerator setMuti()
    {
        yield return new WaitForSeconds(1);
        Event.emit(Events.setMultiplier, null);
    }
}
