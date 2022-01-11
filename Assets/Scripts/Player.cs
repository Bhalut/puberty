using UnityEngine;
using Utils;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    public SpriteAnimation deathAnimationSequence;
    [SerializeField]private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private Movement _movement;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _movement.SetDirection(Vector2.up);
            Debug.Log("Up");
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _movement.SetDirection(Vector2.right);
        }

        var angle = Mathf.Atan2(_movement.CurrentDirection.y, _movement.CurrentDirection.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void Init()
    {
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
        deathAnimationSequence.enabled = false;
        _movement.Init();
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        enabled = false;
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        _movement.enabled = false;
        deathAnimationSequence.enabled = true;
        deathAnimationSequence.SpriteRenderer.enabled = true;
        deathAnimationSequence.Restart();
    }
}