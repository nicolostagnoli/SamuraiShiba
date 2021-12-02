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
        if(_lifeTime> _maxLifeTime) gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>())
        {
            collision.GetComponent<PlayerStats>().TakeDamage(_projectileDamage);
        }
        hit = true;
        _boxCollider2D.enabled = false;
        //Debug.Log("collision with "+ collision.name);
        Destroy(gameObject);
        //Deactivate();
        //_animator.SetTrigger("explode");
    }
    
    public void SetDirection(Vector3 direction)
    {
        hit = false;
        _lifeTime = 0;
        transform.rotation=Quaternion.FromToRotation(transform.position, -direction + transform.position);
        GetComponent<Rigidbody2D>().velocity = direction * speed;
        gameObject.SetActive(true);
        _boxCollider2D.enabled = true;

        //float localScaleX = transform.localScale.x;
        //if (Mathf.Sign(localScaleX) != _direction.x) {localScaleX = -localScaleX;}
        //transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    
}
