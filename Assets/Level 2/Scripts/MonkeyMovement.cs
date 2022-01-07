using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyMovement : BossMovement {
    //Jump
    public float jumpForce;
    public float jumpForceVariance;
    private float checkRadius = 0.5f;
    private bool _isGrounded;
    private bool _isJumping;

    //move around
    public Transform player;
    public Vector3 desiredPosition;
    public float speed;
    public float chargeSpeed;

    //hang on tree
    private bool isHanging;
    public float timeBetweenHangs;
    public float hangTime;
    private float _timeToGoDown;
    private float _timeToHang;

    //throw bananas
    public GameObject bananaPrefab;
    public float shootTime;
    private float _timeBetweenBananas;
    public float bananaDamage;
    public float bananaVelocity;

    //attack
    public float minTimeBetweenJumps;
    public float maxTimeBetweenJumps;
    private float _jumpInterval; //generated between min and max randomly
    private float _timeToJump;
    private bool attackEnabled;
    public Transform attackPosition;
    public float attackRange;
    public float damage;
    public float attackProbability;

    public Transform feetPos;
    public LayerMask whatIsGround;
    private Rigidbody2D _rb;
    private Animator anim;

    private bool featherDarkMode;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _jumpInterval = maxTimeBetweenJumps;
        _timeToHang = timeBetweenHangs;
        anim = GetComponent<Animator>();
        attackEnabled = false;
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
        _timeToHang += Time.deltaTime;

        //face right direction
        if (transform.position.x > player.position.x) {
            transform.localScale = new Vector2(1, 1);
        }
        else {
            transform.localScale = new Vector2(-1, 1);
        }

        //make damage if attack enabled
        if (attackEnabled) {
            Collider2D[] coll = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, LayerMask.GetMask("Player"));
            if(coll.Length > 0) {
                coll[0].gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }


        if (!isHanging) {
            //jump
            if (_isGrounded && _timeToJump >= _jumpInterval) {
                _jumpInterval = Random.Range(minTimeBetweenJumps, maxTimeBetweenJumps);
                _rb.velocity += Vector2.up * (jumpForce + Random.Range(0, 1f) * jumpForceVariance);
                float rand = Random.Range(0f, 1f);
                if (rand > attackProbability) { 
                    Invoke("LandOnPlayer", 1f);
                }
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
            _timeBetweenBananas += Time.deltaTime;

            if(_timeBetweenBananas >= shootTime) {
                ThrowBanana();
                _timeBetweenBananas = 0;
            }

            if(_timeToGoDown >= hangTime) { //when detach from Tree
                _rb.isKinematic = false;
                isHanging = false;
                anim.SetBool("isHanging", false);
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
            anim.SetBool("isHanging", true);
            _isJumping = false;
            _timeToHang = 0;
            _rb.isKinematic = true;
            _rb.velocity = Vector2.zero;
        }
    }

    private void LandOnPlayer() {
        if (!isHanging) {
            _rb.velocity += ((Vector2)((player.position - transform.position).normalized) * chargeSpeed);
            anim.SetTrigger("Attack");
        }
    }
    private Vector3 PickRandomPosition(float randomRange) {
        Vector3 ret = player.transform.position;
        ret.x += Random.Range(-randomRange, +randomRange);
        ret.y = transform.position.y;
        ret.z = 0;
        return ret;
    }

    void ThrowBanana() {
        GameObject banana = Instantiate(bananaPrefab);
        if(featherDarkMode )banana.GetComponentInChildren<DarkParticleEffect>().activateDarkMode();
        banana.GetComponent<BossProjectyle>().SetDamage(bananaDamage);
        banana.transform.position = transform.position;
        banana.transform.rotation = Quaternion.FromToRotation(banana.transform.right, player.transform.position - banana.transform.position) * Quaternion.Euler(0, 0, Random.Range(-20, 20));
        banana.GetComponent<Rigidbody2D>().velocity = banana.transform.right * bananaVelocity;
    }

    public void EnableAttack() {
        attackEnabled = true;
    }

    public void DisableAttack() {
        attackEnabled = false;
    }

    public override void TriggerDarkMode(float damage, float speed, float projectileDamage)
    {
        featherDarkMode = true;
    }
}