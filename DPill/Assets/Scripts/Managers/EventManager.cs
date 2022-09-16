using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent<bool> OnPlayerOnBase = new UnityEvent<bool>();
    public static UnityEvent OnPlayerDied = new UnityEvent();
    public static UnityEvent<Transform> OnEnemyDied = new UnityEvent<Transform>();
}