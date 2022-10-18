using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> wallParts;
    [SerializeField] private Transform explosionPoint;
    [SerializeField] private float destroyDelay = 3f;

    private bool isDestructed = false;
    [Button]

    public void DestuructWall()
    {
        if (isDestructed) return;
        
        isDestructed = true;
        
        foreach (var item in wallParts)
        {
            item.isKinematic = false;
            item.AddExplosionForce(20f, explosionPoint.position, 3f, 0f, ForceMode.VelocityChange);
        }

        StartCoroutine(DestroyPieces());
    }

    private IEnumerator DestroyPieces() 
    {
        yield return new WaitForSeconds(destroyDelay);

        foreach (var item in wallParts)
        {
            item.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack);
        }

        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
