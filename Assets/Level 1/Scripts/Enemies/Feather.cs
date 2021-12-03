using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float _lifeTime;
    private float _maxLifeTime=3;
    private BoxCollider2D _boxCollider2D;
    private float _projectileDamage=10;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hit) return;
        //transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        _lifeTime += Time.deltaTime;
        if(_lifeTime> _maxLifeTime) Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>())
        {
            collision.GetComponent<PlayerStats>().TakeDamage(_projectileDamage);
        }
        hit = true;
        _boxCollider2D.enabled = false;
        Destroy(gameObject);
    }
    
    public void SetDirection(Vector3 direction)
    {
        hit = false;
        _lifeTime = 0;
        transform.rotation=Quaternion.FromToRotation(transform.right, direction - transform.position);
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        gameObject.SetActive(true);
        _boxCollider2D.enabled = true;

    }
    
    
}
