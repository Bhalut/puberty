using System;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        public Sprite[] sprites = Array.Empty<Sprite>();
        public float animationTime = 0.25f;
        public bool loop = true;
        private int _animationFrame;
        public SpriteRenderer SpriteRenderer { get; private set; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(Advance), animationTime, animationTime);
        }

        private void Advance()
        {
            if (!SpriteRenderer.enabled) return;

            _animationFrame++;

            if (_animationFrame >= sprites.Length && loop) _animationFrame = 0;

            if (_animationFrame >= 0 && _animationFrame < sprites.Length)
                SpriteRenderer.sprite = sprites[_animationFrame];
        }

        public void Restart()
        {
            _animationFrame = -1;

            Advance();
        }
    }
}