using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [Header("Player")] 
        [SerializeField, Range(0.0f, 0.3f)]private float _turnSmoothTime = 0.12f;
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _speedChangeRate = 10.0f;

        private CharacterController _controller;
        private Animator _animator;

        private Vector2 _axis;

        private float _speed;
        private float _targetRotation;
        private float _turnSmoothVelocity;

        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Rotation();
            Move();
        }

        private void Rotation()
        {
            var inputDirection = new Vector3(_axis.x, 0.0f, _axis.y).normalized;

            if (_axis != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
                var rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _turnSmoothVelocity,
                    _turnSmoothTime);

                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }

        private void Move()
        {
            var targetSpeed = _moveSpeed;
            if (_axis == Vector2.zero) targetSpeed = 0.0f;
            
            _speed = Mathf.Lerp(_speed, targetSpeed, Time.deltaTime * _speedChangeRate);
            if (_speed < 0.01f) _speed = 0f;

            var targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime));

            _animator.SetFloat(Speed, _speed);
        }

        private void OnMove(InputValue value)
        {
            _axis = value.Get<Vector2>();
        }

        public void MoveInput(Vector2 axis) => _axis = axis;
    }
}