using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyMovement : MonoBehaviour {
    //Jump
    public float jumpForce;
    public float jumpForceVariance;
    private float checkRadius = 0.5f;
    private bool _isGrounded;
    private bool _isJumping;
    public float minTimeBetweenJumps;
    public float maxTimeBetweenJumps;
    private float _jumpInterval; //generated between min and max randomly
    private float _timeToJump;

    //follow player
    public Transform player;
    public float speed;
    public float chargeSpeed;
    public float timeBetweenMovements;
    private float _timeToMovement;

    //hang on tree
    private bool isHanging;
    public float timeBetweenHangs;
    public float hangTime;
    private float _timeToGoDown;
    private float _timeToHang;

    public Transform feetPos;
    public LayerMask whatIsGround;
    private Rigidbody2D _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _jumpInterval = maxTimeBetweenJumps;
    }
    private void FixedUpdate() {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (_isGrounded) {
            _isJumping = false;
        }
        else {
            _isJumping = true;
        }
    }

    void Update() {
        _timeToJump += Time.deltaTime;
        _timeToMovement += Time.deltaTime;
        _timeToHang += Time.deltaTime;

        //face right direction
        if (transform.position.x > player.position.x) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }


        if (!isHanging) {
            //jump
            if (_isGrounded && _timeToJump >= _jumpInterval) {
                _timeToJump = 0;
                _jumpInterval = Random.Range(minTimeBetweenJumps, maxTimeBetweenJumps);
                _rb.velocity += Vector2.up * (jumpForce + Random.Range(-1f, 1f) * jumpForceVariance);
            }

            //follow player
            if (_timeToMovement < timeBetweenMovements) { //just walk
                _rb.position += (Vector2)((player.position - transform.position).normalized) * speed * Time.deltaTime;
            }
            else { //jump and land on player
                if (!_isJumping) {
                    _rb.velocity += Vector2.up * (jumpForce + Random.Range(-1f, 1f) * jumpForceVariance);
                    Invoke("LandOnPlayer", 0.5f);
                    Invoke("LandOnPlayer", 1f);
                    _timeToMovement = 0;
                    _timeToJump = 0;
                }
            }
        }
        else {
            _timeToGoDown += Time.deltaTime;

            if(_timeToGoDown >= hangTime) { //detach from Tree
                _rb.isKinematic = false;
                isHanging = false;
                _isJumping = false;
                _timeToGoDown = 0;
                _timeToHang = 0;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (_isJumping && collision.gameObject.layer == LayerMask.NameToLayer("HangTree") && _timeToHang >= timeBetweenHangs) {
            isHanging = true;
            _isJumping = false;
            _timeToHang = 0;
            _rb.isKinematic = true;
            _rb.velocity = Vector2.zero;
        }
    }

    private void LandOnPlayer() {
        if (!isHanging) {
            _rb.velocity += ((Vector2)((player.position - transform.position).normalized) * chargeSpeed);
        }
    }

}