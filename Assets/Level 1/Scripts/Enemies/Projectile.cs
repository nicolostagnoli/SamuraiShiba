using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float _lifeTime;
    private float _maxLifeTime=3;
    private CircleCollider2D _boxCollider2D;
    private Animator _animator;
    [SerializeField]
    private float _projectileDamage;

    private void Awake()
    {
        _boxCollider2D = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
        _animator.Play("Shot");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime*direction;
        transform.Translate(movementSpeed, 0 , 0);
        _lifeTime += Time.deltaTime;
        if(_lifeTime> _maxLifeTime) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            collision.GetComponent<Enemy>().TakeDamage(_projectileDamage);
        }
        hit = true;
        _boxCollider2D.enabled = false;
        Deactivate();
        //_animator.SetTrigger("explode");
    }

    public void SetDirection(float _direction)
    {
        _lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        _boxCollider2D.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) {localScaleX = -localScaleX;}

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
