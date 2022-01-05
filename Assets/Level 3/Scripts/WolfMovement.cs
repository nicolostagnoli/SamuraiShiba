using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class WolfMovement : MonoBehaviour
{
    public GameObject shiba;
    private Rigidbody2D _rb;
    public LayerMask whatIsGround;
    public Transform feetPos;
    //private bool _isShibaInRange;
    private bool _isGrounded;
    private bool _isReadyToAttack;
    private float _timeToAttack = 3f;
    
    //Teleport
    [Header("Teleport")]
    public List<GameObject> teleportPoints = new List<GameObject>();
    private float _timeToTeleport = 5f;

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
    private float _radiusSpawner = 10f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isReadyToAttack = true;
    }
    void Update()
    {
        _timeToAttack += Time.deltaTime;
        _timeToTeleport += Time.deltaTime;

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
        
        //Teleport in a random spot every 5 seconds
        if (_timeToTeleport >= 10f)
        {
            Teleport();
        }
        
        
        //Choose a random attack from DASH, CLONES, KUNAI

        if (_timeToAttack >= 4f && _isReadyToAttack)
        {
            //check distance from Shiba
            float distFromShiba = Vector3.Distance(shiba.transform.position,transform.position);
            if (distFromShiba >= 3f)
            {
                float randomValue = Random.Range(0, 2);
                if (randomValue == 0)
                {
                    _timeToAttack = 0f;
                    print("KUNAI");
                    ThrowShadowKunai();
                }
                else
                {
                    //Clone courutine
                    print("CLONES");
                    _timeToAttack = 0f;
                    SpawnClones(4);
                    
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
                print("DASH");
                _timeToAttack = 0f;
                DashAttack();
            }
        }
    }
    
    
    //Teleport in a different location then chose an attack
    void Teleport()
    {
        _isReadyToAttack = false;
        print("Teletrasporto");
        _timeToTeleport = 0f;
        int pointIndex = Random.Range(0, teleportPoints.Count);
        bool _isInScreen = false;
        while (!_isInScreen)
        {
            Renderer renderer = teleportPoints[pointIndex].GetComponent<Renderer>();
            if (renderer.isVisible)
            {
                print("Il punto era visibile mi teletrasporto");
                transform.position = teleportPoints[pointIndex].transform.position;
                _isInScreen = true;
            }
            else
            {
                print("Il punto non era a schermo, ne scelgo un altro");
                pointIndex = Random.Range(0, teleportPoints.Count);
                _isInScreen = false;
            }
            
        }
        
        //The bool _isReadyToAttack must be changed based on the animation
        _isReadyToAttack = true;
    }

    /*void MeleeAttack()
    {
        //Add the animation of melee attack
    }*/
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
            float spawnPoint = Random.Range(-_radiusSpawner, _radiusSpawner);
            wolfClone.transform.position = new Vector3(shiba.transform.position.x + spawnPoint, transform.position.y,
                transform.position.z);
        }
    }
}
