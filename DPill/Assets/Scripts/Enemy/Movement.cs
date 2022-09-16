using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]

    public class Movement : MonoBehaviour
    {
        [SerializeField] private int _damage;
    
        private Animator _animator;
        private NavMeshAgent _agent;
        private Player.Health _player;

        private Vector2 _leftBottom, _rightTop;
        private Vector3 _destination;

        private bool _playerOnBase = true;
        private bool _attackTracking;
    
        private static readonly int IsAttack = Animator.StringToHash("isAttack");

        public void Init(Player.Health player, Vector2 leftBottom, Vector2 rightTop)
        {
            _player = player;
            _leftBottom = leftBottom;
            _rightTop = rightTop;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            EventManager.OnPlayerOnBase.AddListener(OnChangeLocation);
        }

        private void OnChangeLocation(bool onBase)
        {
            _playerOnBase = onBase;
            _agent.SetDestination(transform.position);
        }

        private void Update()
        {
            if (_playerOnBase)
            {
                if (_agent.remainingDistance > _agent.stoppingDistance) return;
            
                var path = new Vector3(Random.Range(_leftBottom.x, _rightTop.x), 0, Random.Range(_leftBottom.y, _rightTop.y));
                _agent.SetDestination(path);
            }
            else
            {
                _agent.SetDestination(_player.transform.position);
                var isAttack = _agent.remainingDistance <= _agent.stoppingDistance; 
                _animator.SetBool(IsAttack, isAttack);

                if (!isAttack || !_attackTracking) return;
            
                _player.TakeDamage(_damage);
                _attackTracking = false;
            }
        }

        public void PlayerDied()
        {
            _playerOnBase = true;
            _animator.SetBool(IsAttack, false);
        }
        
        public void ChangePlayer(Player.Health player)
        {
            _player = player;
        }
    
        public void StartAttackTracking()
        {
            _attackTracking = true;
        }
    
        public void StopAttackTracking()
        {
            _attackTracking = false;
        }
    }
}