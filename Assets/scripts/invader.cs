using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class invader : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;
    public System.Action killed;

    [SerializeField] private LayerMask bunkerLayer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        InvokeRepeating(nameof(animateSprite), animationTime, animationTime);
    }
    private void animateSprite()

    {
        _animationFrame++;

        if (_animationFrame >= animationSprites.Length)
        {
            _animationFrame = 0;
        }
        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            this.killed.Invoke();
            this.gameObject.SetActive(false);

        }

        if (other.gameObject.layer == LayerMask.NameToLayer("bunkers"))
        {
            Debug.Log("Bunker trigger detected!");
            GameOver();
        }
    }
    private void GameOver()
    {
        Debug.Log("Invaders reached bunkers! Game Over.");
        SceneManager.LoadScene("GameOver");
    }
}
