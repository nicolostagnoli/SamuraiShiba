using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerWithBoxCollider : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    private float _moveSpeed;
    private float _jumpForce;
    private float _moveHorizontal;
    private float _moveVertical;
    private bool _isJumping;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        
        _moveSpeed = 3f;
        _jumpForce = 20f;
        _isJumping = false;
    }

    private void Update()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (_moveHorizontal > 0.1f || _moveHorizontal < -0.1f)
        {
            _rb.AddForce(new Vector2(_moveHorizontal * _moveSpeed, 0), ForceMode2D.Impulse);
        }
        if (!_isJumping && _moveVertical > 0.1f)
        {
            _rb.AddForce(new Vector2(0f, _moveVertical * _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            _isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            _isJumping = true;
        }
    }
}
