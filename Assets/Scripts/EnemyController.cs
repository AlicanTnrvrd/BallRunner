using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private EnemyAnimationController enemyAnimController;
    [SerializeField] private new Collider collider;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image healthImage;
    [SerializeField] private Transform healthBarParent;
    
    
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        canvas.worldCamera= GameManager.Instance.GetCamera();
        FillHealthBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyAnimController.PlayDanceAnim();
        }

        if (other.CompareTag("Shield"))
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        FillHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        enemyAnimController.PlayDieAnim();
        collider.enabled = false;
        CloseHealthBar();
    }

    private void FillHealthBar()
    {
        var ratio = currentHealth / maxHealth;
       // var ratio2= Mathf.InverseLerp(0,1,ratio);
        healthImage.fillAmount = ratio;
    }

    private void CloseHealthBar()
    {
        healthBarParent.DOScale(0f, 1f)
            .SetEase(Ease.InBack);
    }
}
