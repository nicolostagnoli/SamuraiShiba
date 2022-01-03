using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WolfMovement : MonoBehaviour
{
    public GameObject shiba;
    private Rigidbody2D _rb;
    public LayerMask whatIsGround;
    public Transform feetPos;
    //private bool _isShibaInRange;
    private bool _isGrounded;
    private float _timeToAttack = 5f;
    
    //DashAttack
    [Header("DashAttack")]
    //private float checkRadius = 0.5f;
    //private float _timeDashing;
    //private float _timeToDash = 2f;
    private float _dashSpeed = 5f;
    private float _facingDirection;
    //private bool _isDashing;
    
    //ShadowKunai
    [Header("ShadowKunai")] 
    public GameObject shadowKunaiPrefab;
    public float shadowKunaiVelocity;
    //public float shadowKunaiDamage;
    //public float maxShootTime;
    //public float minShootTime;
    
    //Clones
    [Header("Clones")]
    public GameObject wolfClonePrefab;
    private float _radiusSpawner;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }
    void Update()
    {
        _timeToAttack += Time.deltaTime;

        if (transform.position.x > shiba.transform.position.x)
        {
            _facingDirection = -1f;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            _facingDirection = 1f;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
        //Choose a random attack from DASH, CLONES, KUNAI

        if (_timeToAttack >= 5f)
        {
            //check distance from Shiba
            float distFromShiba = Vector3.Distance(shiba.transform.position,transform.position);
            if (distFromShiba >= 5f)
            {
                float randomValue = Random.Range(0, 2);
                if (randomValue == 0)
                {
                    ThrowShadowKunai();
                }
                else
                {
                    //Clone courutine
                    print("CLONES");
                    _timeToAttack = 0f;
                }
            }
            else if (distFromShiba <= 1f)
            {
                print("MELEE");
                _timeToAttack = 0f;
                //Melee attack
            }
            else
            {
                DashAttack();
            }
        }
    }

    void DashAttack()
    {
        _timeToAttack = 0f;
        Vector2 dashDirection = shiba.transform.position - transform.position;
        _rb.AddForce(dashDirection * _dashSpeed, ForceMode2D.Impulse);
    }

    void ThrowShadowKunai()
    {
        _timeToAttack = 0f;
        GameObject shadowKunai = Instantiate(shadowKunaiPrefab);
        shadowKunai.transform.position = transform.position;
        shadowKunai.transform.rotation = Quaternion.FromToRotation(shadowKunai.transform.right, shiba.transform.position - shadowKunai.transform.position);
        shadowKunai.GetComponent<Rigidbody2D>().velocity = shadowKunai.transform.right * shadowKunaiVelocity;
    }

    void SpawnClones(int numClones)
    {
        _timeToAttack = 0f;
        for (int i = 1; i <= numClones; i++)
        {
            GameObject wolfClone = Instantiate(wolfClonePrefab);
            float spawnPoint = Random.Range(0f, _radiusSpawner);
            wolfClone.transform.position = new Vector3(transform.position.x + spawnPoint, transform.position.y,
                transform.position.z);
        }
    }
}
