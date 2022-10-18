using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Transform visual;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private float range = 20f;

    private Vector3 startPos;
    private Vector3 maxPos;
    private bool isMoveable;


    private void Update()
    {
        if (isMoveable)
        {
            Move();
            CheckMaxRange();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.attachedRigidbody.GetComponent<EnemyController>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            isMoveable = false;
        }
    }
    private void Move()
    {
        transform.position += Vector3.forward * bulletSpeed * Time.deltaTime;
    }

    public void Shoot()
    {
        isMoveable = true;
    }

    public void SetPosition(Vector3 pos)
    {
        startPos = pos;
        maxPos = startPos + Vector3.forward * range;
    }

    private void CheckMaxRange()
    {
        if (maxPos.z <= transform.position.z)
        {
            Destroy(gameObject);
        }
    }
    
    
    


}


