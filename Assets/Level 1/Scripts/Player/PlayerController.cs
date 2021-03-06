
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  
  //Components
  private Rigidbody2D _rb;
  private Animator _animator;
  public LayerMask whatIsGround;
  public Transform feetPos;
  public GameObject anim;
  public ParticleSystem dust;
  private PlayerStats _stats;

  //Horizontal Movement
  public float speed;
  private float _moveInput;
  private float _facingDirection;
  private bool _canMove;
  private float _previousFacingDirection;
  
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
    public float dashStamina;
  
  public float dashTime;
  public float dashSpeed;
  public float distanceBetweenImages;
  public float dashCooldown;
  
  private void Start()
  {
    _rb = GetComponent<Rigidbody2D>();
    _canMove = true;
    //_animator = anim.GetComponent<Animator>();
    _animator = GetComponent<Animator>();
        _stats = GetComponent<PlayerStats>();
        //jumpForce = StateNameController.jumpForce;
        //dashSpeed = StateNameController.dashSpeed;
  }

  private void FixedUpdate()
  {
    _moveInput = Input.GetAxisRaw("Horizontal");
    if (_canMove)
    {
      _rb.velocity = new Vector2(_moveInput * speed, _rb.velocity.y);
      _animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
    }
  }

  private void Update()
  {
    CheckDash();
    _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    if(_isGrounded == true)
      _animator.SetBool("isJumping", false);

    //Turn left and right the sprite
    if (_moveInput > 0)
    {
      transform.eulerAngles = new Vector3(0, 0, 0);
      _facingDirection = 1f;
      if (_previousFacingDirection != _facingDirection)
      {
        CreateDust();
      }
      _previousFacingDirection = _facingDirection;
    }
    else if(_moveInput < 0)
    {
      transform.eulerAngles = new Vector3(0, 180, 0);
      _facingDirection = -1f;
      if (_previousFacingDirection != _facingDirection)
      {
        CreateDust();
      }
      _previousFacingDirection = _facingDirection;
    }
    
    //TODO: check when player returns on ground to stop jump animation
    //Jumping with high jump
    if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
    {
      _isJumping = true;
      CreateDust();
      _animator.SetTrigger("Jump");
      _animator.SetBool("isJumping", true);
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
      if(Time.time >= (_lastDash + dashCooldown) && _stats.getStamina() >= dashStamina)
        AttemptToDash();
    }
    
  }

  private void AttemptToDash()
  {
    Debug.Log("Sono in attemptToDash");
    _isDashing = true;
    _dashTimeLeft = dashTime;
    _lastDash = Time.time;
        _stats.UseStamina(dashStamina);

    //PlayerAfterImagePool.Instance.GetFromPool();
    //_lastImageXPos = transform.position.x;
  }

  public bool CanUseShuriken()
  {
    return true;
  }

  private void CheckDash()
  {
    if (_isDashing)
    {
      if (_dashTimeLeft > 0)
      {
        _canMove = false;
        //canFlip = false;
        //_rb.velocity = new Vector2(dashSpeed * -_facingDirection, _rb.velocity.y);
        //Debug.Log(_rb.velocity);
        _dashTimeLeft -= Time.deltaTime;
        if (_moveInput == 0)
        {
          _animator.SetTrigger("Dodge");
          _rb.velocity = new Vector2(dashSpeed * -_facingDirection, _rb.velocity.y);
        }
        else
        {
          _animator.SetTrigger("Roll");
          _rb.velocity = new Vector2(dashSpeed * 2 * _facingDirection, _rb.velocity.y);
        }
        /*if (Mathf.Abs(transform.position.x - _lastImageXPos) > distanceBetweenImages)
        {
          PlayerAfterImagePool.Instance.GetFromPool();
          _lastImageXPos = transform.position.x;
        }*/
      }

      if (_dashTimeLeft <= 0)
      {
        _isDashing = false;
        _canMove = true;
       // canFlip = true;

      }
    }
  }

  private void CreateDust()
  {
    dust.Play();
  }
}
