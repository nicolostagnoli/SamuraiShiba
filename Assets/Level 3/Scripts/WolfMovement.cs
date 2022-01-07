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
    private Animator anim;
    public LayerMask whatIsGround;
    public Transform feetPos;
    //private bool _isShibaInRange;
    private bool _isGrounded;
    private bool _isReadyToAttack;
    private float _timeToAttack = 3f;
    
    //Teleport
    [Header("Teleport")]
    public List<GameObject> teleportPoints = new List<GameObject>();
    private float _timeToTeleport = 3f;
    private Renderer _wolfRenderer;

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
        _wolfRenderer = GetComponentInChildren<Renderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        _timeToAttack += Time.deltaTime;
        _timeToTeleport += Time.deltaTime;
        print("Time to teleport: " + _timeToTeleport);

        //check distance from Shiba
        float distFromShiba = Vector3.Distance(shiba.transform.position,transform.position);
        print("DIST from Shiba: " + distFromShiba);
        
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

        if (distFromShiba >= 7f)
        {
            _timeToTeleport = 6f;
        }
        
        //Teleport in a random spot every 5 seconds
        if (_timeToTeleport >= 6f)
        {
            anim.SetTrigger("Teleport");
            //Teleport();
        }
        
        
        //Choose a random attack from DASH, CLONES, KUNAI

        if (_timeToAttack >= 4f && _isReadyToAttack)
        {
            if (distFromShiba > 2f)
            {
                float randomValue = Random.Range(0, 2);
                if (randomValue == 0)
                {
                    _timeToAttack = 0f;
                    anim.SetTrigger("Kunai");
                    //ThrowShadowKunai();
                }
                else
                {
                    _timeToAttack = 0f;
                    anim.SetTrigger("Clone");
                    //SpawnClones(4);
                }
            }
            else
            {
                _timeToAttack = 3.2f;
                anim.SetTrigger("Melee");
                //Melee attack
            }
        }
    }
    
    //Teleport in a different location then chose an attack
    void Teleport()
    {
        _isReadyToAttack = false;
        _timeToTeleport = 0f;
        int pointIndex = Random.Range(0, teleportPoints.Count);
        bool isInScreen = false;
        while (!isInScreen)
        {
            Renderer renderer = teleportPoints[pointIndex].GetComponent<Renderer>();
            if (renderer.isVisible)
            {
                transform.position = teleportPoints[pointIndex].transform.position;
                print("Teletrasporto");
                isInScreen = true;
            }
            else
            {
                pointIndex = Random.Range(0, teleportPoints.Count);
            }
            
        }
        
        //The bool _isReadyToAttack must be changed based on the animation
        _isReadyToAttack = true;
    }

    /*void MeleeAttack()
    {
        //Add the animation of melee attack
    }*/
    /*void DashAttack()
    {
        _timeToAttack = 0f;
        Vector2 dashDirection = shiba.transform.position - transform.position;
        _rb.AddForce(dashDirection * _dashSpeed, ForceMode2D.Impulse);
    }*/

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
