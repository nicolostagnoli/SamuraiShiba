using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class DarkParticle : MonoBehaviour
{
    
    public Vector2 minVelocity = new Vector2(-0.05f, 0.1f);
    public Vector2 maxVelocity = new Vector2(0.05f, 0.2f);

    public float lifeSpan = 0.1f;
    private float timeAlive;
    private float actualLifeSpan;
    private Vector2 velocity;
    // Start is called before the first frame update
    void OnEnable()
    {
        velocity = new Vector2(Random.Range(minVelocity.x, maxVelocity.x), Random.Range(minVelocity.y, maxVelocity.y));
        actualLifeSpan = lifeSpan * Random.Range(0.9f, 1.1f);
        timeAlive = 0;
    }

    

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= actualLifeSpan)
        {
            SimplePool.Despawn(gameObject);
            return;
        }
        transform.Translate(velocity*Time.deltaTime);
    }
}
