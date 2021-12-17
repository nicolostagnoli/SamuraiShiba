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
        Destroy(gameObject);
    }
    
    public void SetDirection(Vector3 direction)
    {
        hit = false;
        _lifeTime = 0;
        Vector3 dir = (direction- transform.position).normalized;
        Vector3 temp = transform.position-direction;
        //Rotation the feather towards the player
        transform.rotation= Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y,180+Mathf.Atan2(temp.y,temp.x)*Mathf.Rad2Deg);
        GetComponent<Rigidbody2D>().velocity = dir * speed;
        gameObject.SetActive(true);
    }
}
