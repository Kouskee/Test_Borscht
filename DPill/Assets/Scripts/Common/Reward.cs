using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class Reward : MonoBehaviour
{
    [SerializeField] private float _delayToDestroy;
    [SerializeField] private Vector2Int _rangeReward;
    public UnityEvent<int> OnPickUp;

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
        if (!other.GetComponent<Movement>()) return;
        
        OnPickUp.Invoke(Random.Range(_rangeReward.x, _rangeReward.y+1));
        Destroy(gameObject, 0.1f);
    }
}
