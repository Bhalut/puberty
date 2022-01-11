using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        [Header("Speed")] public float speed = 8.0f;

        public float speedMultiplier = 1.0f;

        [Header("Direction")] public Vector2 initialDirection;

        [Header("Layer")] public LayerMask wallLayer;

        private Vector2 _nextDirection;
        private Vector3 _startingPosition;

        public Rigidbody2D Rigidbody { get; private set; }
        public Vector2 CurrentDirection { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_nextDirection != Vector2.zero) SetDirection(_nextDirection);
        }

        private void FixedUpdate()
        {
            var position = Rigidbody.position;
            var translation = CurrentDirection * (speed * speedMultiplier * Time.fixedDeltaTime);

            Rigidbody.MovePosition(position + translation);
        }

        public void Init()
        {
            var transformMovement = transform;
            _startingPosition = transformMovement.position;
            transformMovement.position = _startingPosition;

            speedMultiplier = 1.0f;
            CurrentDirection = initialDirection;
            _nextDirection = Vector2.zero;
            Rigidbody.isKinematic = false;
            enabled = true;
        }

        public void SetDirection(Vector2 direction, bool forced = false)
        {
            if (forced || !IsContinue(direction))
            {
                CurrentDirection = direction;
                _nextDirection = Vector2.zero;
            }
            else
            {
                _nextDirection = direction;
            }
        }

        private bool IsContinue(Vector2 direction)
        {
            var hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, wallLayer);
            return hit.collider != null;
        }
    }
}