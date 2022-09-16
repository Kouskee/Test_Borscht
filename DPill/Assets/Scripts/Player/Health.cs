using UnityEngine;

namespace Player
{
   public class Health : MonoBehaviour
   {
      [SerializeField] private int _health;
   
      public void TakeDamage(int damage)
      {
         _health -= damage;
      
         if(_health<= 0) EventManager.OnPlayerDied.Invoke();
      }
   }
}
