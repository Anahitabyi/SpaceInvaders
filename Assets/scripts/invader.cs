using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
// Class representing a single invader in the alien army
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
        //start repeating animation
        InvokeRepeating(nameof(animateSprite), animationTime, animationTime);
        //get the refrence to the scoremanger ui and store it.
        this.ScoreUI = GameObject.Find("ScoreManager").GetComponent <ScoreUI>();

    }
    private void animateSprite()

    {
        //advance to next animation frame
        _animationFrame++;

        // Loop back to first frame if we've reached the end
        if (_animationFrame >= animationSprites.Length)
        {
            _animationFrame = 0;
        }
        //uodate the sprite renderer with curresnt frame
        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            //play the explosion of invader animation
            if (ExplosionPrefab != null)
            {
                GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 0.25f);
            }
            //update the ui of the score
            this.ScoreUI.UpdateScore(50);
            this.killed.Invoke();
            //inactive the invader gameobject
            this.gameObject.SetActive(false);
            SoundEffectManager.play("killedinvader");

        }

        //if the invaders reach the bunker the gameover screen is played
        if (other.gameObject.layer == LayerMask.NameToLayer("bunkers"))
        {
            Debug.Log("Bunker trigger detected!");
            GameOver();
        }
    }
    //loading the gameover scene
    private void GameOver()
    {
        Debug.Log("Invaders reached bunkers! Game Over.");
        SceneManager.LoadScene("GameOver");
    }
}
