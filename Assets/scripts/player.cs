using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
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
        rb.linearVelocity = moveInput * moveSpeed;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        float clampX = Mathf.Clamp(transform.position.x, leftEdge.x + 1.0f, rightEdge.x - 1.0f);
        transform.position = new Vector3(clampX, transform.position.y, transform.position.z);


    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_laserActive)
            {
                projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
                projectile.destroyed += LaserDestroyed;
                _laserActive = true;
            }

        }
    }
    private void LaserDestroyed()
    {
        _laserActive = false;
    }

}

