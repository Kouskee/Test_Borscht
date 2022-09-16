using Cinemachine;
using Player;
using UnityEngine;
using Movement = Enemy.Movement;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private SpawnerEnemies _spawner;
    [SerializeField] private GameManager _manager;
    [Header("Characters")]
    [SerializeField] private Health _player;
    [SerializeField] private Movement _enemy;
    [Header("Common")] 
    [SerializeField] private Reward _reward;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private void Awake()
    {
        _spawner.Init(_enemy);
        _manager.Init(_spawner, _player, _camera, _reward);
        Destroy(gameObject);
    }
}
