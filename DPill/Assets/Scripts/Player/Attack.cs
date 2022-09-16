using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float _delayFiring;
        [SerializeField] private Rigidbody _projectile;
        
        private List<Enemy.Movement> _enemies;
        private Enemy.Movement _nearestEnemy;

        public void Init(List<Enemy.Movement> enemies)
        {
            _enemies = enemies;
        }

        private void Start()
        {
            EventManager.OnEnemyDied.AddListener(EnemyDied);
        }

        private void EnemyDied(Transform enemyTransform)
        {
            _enemies.Remove(enemyTransform.GetComponent<Enemy.Movement>());
        }

        public IEnumerator StartShootingRoutine()
        {
            while (true)
            {
                if(_enemies.Count <= 0) break;
                
                var minDistance = float.MaxValue;
                for (var i = 0; i < _enemies.Count; i++)
                {
                    var distance = Vector3.Distance(_enemies[i].transform.position, transform.position);
                
                    if (!(distance < minDistance)) continue;
                
                    minDistance = distance;
                    _nearestEnemy = _enemies[i];
                }
                Shoot();

                yield return new WaitForSeconds(_delayFiring);
            }
        }

        private void Shoot()
        {
            var position = transform.position;
            var newBullet = Instantiate(_projectile, position + Vector3.up * 1, Quaternion.identity);
            var direction = _nearestEnemy.transform.position - position;

            newBullet.velocity = direction * 2f;
        }
    }
}