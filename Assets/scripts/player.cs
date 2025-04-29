using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    // Player class that handles player movement, shooting, and screen boundary constraints
    public projectile laserPrefab;
    [SerializeField] private float moveSpeed = 5.0f;
    private Rigidbody2D rb;
    private PlayerHealth health;
    private Vector2 moveInput;
    private bool _laserActive;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHealth>();
    }

    private void Update()
    {   
        // Apply movements and clamp positions to screen edges.0(with 1 unit padding)0
        rb.linearVelocity = moveInput * moveSpeed;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        float clampX = Mathf.Clamp(transform.position.x, leftEdge.x + 1.0f, rightEdge.x - 1.0f);
        transform.position = new Vector3(clampX, transform.position.y, transform.position.z);


    }

    //this method is called by input system when movement keys are pressed
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //this method is called by input system when fire buuton is pressed
    public void shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_laserActive)
            {
                //create laser, play the laser soundeffect and  sets up an event subscription between the laser projectile and the player. 
                projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
                SoundEffectManager.play("shoot");
                projectile.destroyed += LaserDestroyed;
                _laserActive = true;
            }

        }
    }
    //this method is called when a projectile is destroyed. (by event subscription between the laser in projectile class and this method.)
    private void LaserDestroyed()
    {
        _laserActive = false;
    }

}

