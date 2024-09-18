using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlappyBird : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private InputActionReference space;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) 
        { 
            rb.velocity = Vector2.up * velocity;
        }

        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != null)
        {
            GameManager.Instance.GameOver();
        }
    }

}
