using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  
  //Components
  private Rigidbody2D _rb;
  public LayerMask whatIsGround;
  public Transform feetPos;
 
  //Horizontal Movement
  public float speed;
  private float _moveInput;
  private float _facingDirection;
  private bool _canMove;
  
  //Jump
  public float jumpForce;
  public float checkRadius;
  public float jumpTime;
  private float _jumpTimeCounter;
  private bool _isGrounded;
  private bool _isJumping;
  
  //Dash
  private float _dashTimeLeft;
  private float _lastImageXPos;
  private float _lastDash = -100;
  private bool _isDashing;
  
  public float dashTime;
  public float dashSpeed;
  public float distanceBetweenImages;
  public float dashCooldown;
  
  private void Start()
  {
    _rb = GetComponent<Rigidbody2D>();
    _canMove = true;
  }

  private void FixedUpdate()
  {
    _moveInput = Input.GetAxisRaw("Horizontal");
    if (_canMove)
    {
      _rb.velocity = new Vector2(_moveInput * speed, _rb.velocity.y);
    }
  }

  private void Update()
  {
    Debug.Log(_isDashing);
    
    CheckDash();
    _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

    //Turn left and right the sprite
    if (_moveInput > 0)
    {
      transform.eulerAngles = new Vector3(0, 0, 0);
      _facingDirection = 1f;
    }
    else if(_moveInput < 0)
    {
      transform.eulerAngles = new Vector3(0, 180, 0);
      _facingDirection = -1f;
    }
    
    
    //Jumping with high jump
    if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
    {
      _isJumping = true;
      _jumpTimeCounter = jumpTime;
      _rb.velocity = Vector2.up * jumpForce;
    }

    if (Input.GetKey(KeyCode.Space) && _isJumping)
    {
      if (_jumpTimeCounter > 0)
      {
        _rb.velocity = Vector2.up * jumpForce;      
        _jumpTimeCounter -= Time.deltaTime;
      }
      else
      {
        _isJumping = false;
      }
    }

    if (Input.GetKeyUp(KeyCode.Space))
    {
      _isJumping = false;
    }
    
    //Dashing
    if (Input.GetKey(KeyCode.LeftShift))
    {
      Debug.Log("Ho premuto shift");
      if(Time.time >= (_lastDash + dashCooldown))
        AttemptToDash();
    }
    
  }

  private void AttemptToDash()
  {
    Debug.Log("Sono in attemptToDash");
    _isDashing = true;
    _dashTimeLeft = dashTime;
    _lastDash = Time.time;

    PlayerAfterImagePool.Instance.GetFromPool();
    _lastImageXPos = transform.position.x;
  }

  private void CheckDash()
  {
    if (_isDashing)
    {
      if (_dashTimeLeft > 0)
      {
        _canMove = false;
        //canFlip = false;
        _rb.velocity = new Vector2(dashSpeed * _facingDirection, _rb.velocity.y);
        Debug.Log(_rb.velocity);
        _dashTimeLeft -= Time.deltaTime;

        if (Mathf.Abs(transform.position.x - _lastImageXPos) > distanceBetweenImages)
        {
          PlayerAfterImagePool.Instance.GetFromPool();
          _lastImageXPos = transform.position.x;
        }
      }

      if (_dashTimeLeft <= 0)
      {
        _isDashing = false;
        _canMove = true;
       // canFlip = true;

      }
    }
  }
}
