using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float direction;
    private bool hit;
    private float _lifeTime;
    private float _maxLifeTime=3;
    private Animator _animator;
    [SerializeField]
    private float _projectileDamage;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("Shot");
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        _lifeTime += Time.deltaTime;
        //transform.Translate(new Vector3(speed*Time.deltaTime*-direction,0));
        //transform.Translate(new Vector2(1f, 0f) *speed* Time.deltaTime*direction);
        _rigidbody2D.velocity=new Vector2(speed*direction,0 );
        //Debug.Log("direction " + direction);
        if(_lifeTime> _maxLifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            collision.GetComponent<Enemy>().TakeDamage(_projectileDamage);
        }
        hit = true;
        Destroy(gameObject);
    }

    public void SetDirection(float _direction)
    {
        hit = false;
        _lifeTime = 0;
        direction = _direction;
        Debug.Log("direction " +direction);
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) {localScaleX = -localScaleX;}
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        gameObject.SetActive(true);

    }
}
