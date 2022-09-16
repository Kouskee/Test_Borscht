using Player;
using UnityEngine;

public class ChangeLocation : MonoBehaviour
{
    private Attack _player;
    private Coroutine _coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out _player)) return;
        
        EventManager.OnPlayerOnBase.Invoke(true);
        if(_coroutine != null) _player.StopCoroutine(_coroutine);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out _player)) return;
        
        EventManager.OnPlayerOnBase.Invoke(false);
        _coroutine = _player.StartCoroutine(_player.StartShootingRoutine());
    }
}
