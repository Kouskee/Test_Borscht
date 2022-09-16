using System.Collections;
using Enemy;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delayToDestroy;

    private void Start()
    {
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(_delayToDestroy);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Health health)) return;
        health.TakeDamage(_damage);
        Destroy(gameObject);
    }
}