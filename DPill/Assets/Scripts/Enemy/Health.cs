using UnityEngine;

namespace Enemy
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _health;
   
        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health > 0) return;
            EventManager.OnEnemyDied.Invoke(transform);
            Destroy(gameObject);
        }
    }
}