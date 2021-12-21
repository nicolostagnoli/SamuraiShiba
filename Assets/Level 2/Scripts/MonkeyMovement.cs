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

    //move around
    public Transform player;
    public Vector3 desiredPosition;
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
        _timeToHang = timeBetweenHangs;
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
                _jumpInterval = Random.Range(minTimeBetweenJumps, maxTimeBetweenJumps);
                _rb.velocity += Vector2.up * (jumpForce + Random.Range(0, 1f) * jumpForceVariance);
                Invoke("LandOnPlayer", 1f);
                _timeToJump = 0;
            }

            //move toward desired position
            _rb.position += (Vector2)((desiredPosition - transform.position).normalized) * speed * Time.deltaTime;

            //follow player
            if (Mathf.Abs(transform.position.x - desiredPosition.x) <= 1) {
                desiredPosition = PickRandomPosition(5);
            }
        }
        else { //here hanging on the tree
            _timeToGoDown += Time.deltaTime;

            if(_timeToGoDown >= hangTime) { //detach from Tree
                _rb.isKinematic = false;
                isHanging = false;
                _isJumping = false;
                _timeToGoDown = 0;
                _timeToHang = 0;

                //make a jump when detaching
                _rb.velocity += Vector2.up * (jumpForce + Random.Range(0, 0.2f) * jumpForceVariance) + (Vector2)transform.right * jumpForce/2;
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
    private Vector3 PickRandomPosition(float randomRange) {
        Vector3 ret = player.transform.position;
        ret.x += Random.Range(-randomRange, +randomRange);
        ret.y = transform.position.y;
        ret.z = 0;
        return ret;
    }

}