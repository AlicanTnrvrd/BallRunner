using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayDanceAnim()
    {
        animator.SetTrigger("Dance");


    }

    public void PlayDieAnim()
    {
        animator.SetTrigger("Die");


    }

   
}
