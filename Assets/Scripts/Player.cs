using System;
using Q_A;
using Scriptable;
using UnityEngine;
using Utils;

public class Player : MonoBehaviour
{

    [Header("Animation")] public SpriteAnimation deathAnimationSequence;

    [Header("Speed")] public float speed = 1.5f;

    public float speedMultiplier = 1.0f;

    [Header("Direction")] public Vector2 initialDirection = Vector2.right;

    [Header("Data")] public GameStateData state;

    private Collider2D _collider;

    private Vector2 _currentDirection;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _startingPosition;
    public Action<bool> OnAnswerSuccess;


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!state.onGame) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            _currentDirection = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            _currentDirection = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            _currentDirection = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) _currentDirection = Vector2.right;

        var angle = Mathf.Atan2(_currentDirection.y, _currentDirection.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    private void FixedUpdate()
    {
        if (!state.onGame) return;

        var position = _rigidbody.position;
        var translation = _currentDirection * (speed * speedMultiplier * Time.fixedDeltaTime);

        _rigidbody.MovePosition(position + translation);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.TryGetComponent(out Answer answer)) return;

        if (answer.data.isCorrect) OnAnswerSuccess(answer.data.isCorrect);
    }

    public void Init()
    {
        var transformMovement = transform;
        _startingPosition = transformMovement.position;
        transformMovement.position = _startingPosition;

        speedMultiplier = 1.0f;
        _currentDirection = initialDirection;
        _rigidbody.isKinematic = false;

        _spriteRenderer.enabled = true;
        _collider.enabled = true;
        deathAnimationSequence.enabled = false;
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        deathAnimationSequence.enabled = true;
        deathAnimationSequence.SpriteRenderer.enabled = true;
        deathAnimationSequence.Restart();
    }
}