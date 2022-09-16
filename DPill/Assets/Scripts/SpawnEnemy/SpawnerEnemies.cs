using System.Collections.Generic;
using Player;
using UnityEngine;
using Movement = Enemy.Movement;
using Random = UnityEngine.Random;

public class SpawnerEnemies : MonoBehaviour
{
    [SerializeField] private int _countEnemies;
    [SerializeField] private Transform[] _zoneEdges;

    private List<Movement> _enemies;

    private Health _player;
    private Movement _enemy;
    private Attack _attack;

    public void Init(Movement enemy)
    {
        _enemy = enemy;
    }

    private void Start()
    {
        _enemies = new List<Movement>(_countEnemies);
    }

    public void PlayerDied()
    {
        if (_enemies == null) return;
        foreach (var enemy in _enemies)
        {
            enemy.PlayerDied();
        }
    }
    
    public void NewPlayer(Health player)
    {
        _player = player;
        _attack = _player.GetComponent<Attack>();
        if (_enemies != null)
        {
            foreach (var enemy in _enemies)
            {
                enemy.ChangePlayer(player);
            }
        }
        Spawn();
    }

    private void Spawn()
    {
        var leftBottom = GetPosition(_zoneEdges[0]);
        var rightTop = GetPosition(_zoneEdges[1]);

        for (int i = 0; i < _countEnemies; i++)
        {
            var position = new Vector3(Random.Range(leftBottom.x, rightTop.x), 0, Random.Range(leftBottom.y, rightTop.y));

            var enemy = Instantiate(_enemy, position, Quaternion.identity, transform);
            enemy.Init(_player, leftBottom, rightTop);
            _enemies.Add(enemy);
        }
        
        _attack.Init(_enemies);

        Vector2 GetPosition(Transform edge)
        {
            return new Vector2(edge.position.x, edge.position.z);
        }
    }

    public void CheckForSpawn()
    {
        if (_enemies.Count <= 1)
            Spawn();
    }
}