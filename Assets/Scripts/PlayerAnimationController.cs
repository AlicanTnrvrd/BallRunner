using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Button]
    public void PlayRunAnim()
    {
        StopAllAnims();
        animator.SetBool("Run",true);

    }

    public void StopRunAnim()
    {
        animator.SetBool("Run", false);

    }
    [Button]
    public void PlayFireAnim()
    {
        StopAllAnims();
        animator.SetBool("Fire", true);

    }

    public void StopFireAnim()
    {
        animator.SetBool("Fire", false);

    }

    private void StopAllAnims()
    {
        StopRunAnim();
        StopFireAnim();

    }

    public void PlayDieAnim()
    {
        StopAllAnims();
        animator.SetTrigger("Die");

    }

    public void PlayDanceAnim()
    {
        StopAllAnims();
        animator.SetTrigger("Dance");

    }

    public void Shoot()// anim event
    {
        PlayerController.Instance.Shoot();
    }
}

