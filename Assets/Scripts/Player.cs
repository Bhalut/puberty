using System;
using Q_A;
using Scriptable;
using UnityEngine;
using Utils;

public class Player : MonoBehaviour
{

    [Header("Animation")] public SpriteAnimation principalAnimation;

    public SpriteAnimation deathAnimation;


    [Header("Speed")] public float speed = 1.5f;

    public float speedMultiplier = 1.0f;

    [Header("Direction")] public Vector2 initialDirection = Vector2.right;

    [Header("Data")] public GameStateData state;
    private readonly Vector3 _startingPosition = new(0.27f, -1.15f, 0);

    private Collider2D _collider;

    private Vector2 _currentDirection;
    private Rigidbody2D _rigidbody;

    public Action<bool> OnAnswerSuccess;
    public Action OnPlayerDamage;


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
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

        if (answer.data.isCorrect)
            OnAnswerSuccess(answer.data.isCorrect);
        else
            OnPlayerDamage();
    }

    public void Init()
    {
        transform.position = _startingPosition;

        speedMultiplier = 1.0f;
        _currentDirection = initialDirection;
        _rigidbody.isKinematic = false;

        principalAnimation.enabled = true;
        principalAnimation.loop = true;
        _collider.enabled = true;
        deathAnimation.enabled = false;
        deathAnimation.loop = false;
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        principalAnimation.enabled = false;
        principalAnimation.loop = false;
        _collider.enabled = false;
        deathAnimation.enabled = true;
        deathAnimation.loop = true;
        deathAnimation.Restart();
    }
}