using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneFeather : MonoBehaviour
{
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFeatherDamage(float d) {
        damage = d;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<PlayerStats>()) {
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
            if (!collision.GetComponent<PlayerStats>().GetInvulnerability()) {
                Destroy(gameObject);
            }
        }
    }
}
