using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
public class invader : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;
    public System.Action killed;
    public GameObject ExplosionPrefab;
    private ScoreUI ScoreUI;
    [SerializeField] private int points;

    [SerializeField] private LayerMask bunkerLayer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        InvokeRepeating(nameof(animateSprite), animationTime, animationTime);
        this.ScoreUI = GameObject.Find("ScoreManager").GetComponent <ScoreUI>();

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
            if (ExplosionPrefab != null)
            {
                GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 1f);
            }
            this.ScoreUI.UpdateScore(50);
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
