using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
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
        _rigidbody2D.velocity = direction*speed;
        if(_lifeTime> _maxLifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.GetComponent<Enemy>())
        {
            collision.GetComponent<Enemy>().TakeDamage(_projectileDamage);
        }
        hit = true;
        Destroy(gameObject);
    }

    public void SetDirection(Vector3 _direction)
    {
        hit = false;
        _lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
    }
}
