using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenAttack : MonoBehaviour
{

    [SerializeField]
    private float _attackCooldown=0.5f;
    //animazione del personaggio che lancia lo shuriken
    private Animator _animator;
    private PlayerController _playerController;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    public GameObject shurikenPrefab;
    private float _cooldownTimer= Mathf.Infinity;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }



    // Update is called once per frame
    void Update()
    {
        _cooldownTimer += Time.deltaTime;
    }
    
    public  void Attack()
    {
        _cooldownTimer = 0;
        shurikenPrefab.GetComponent<Projectile>().SetDirection(gameObject.transform.right);
        Instantiate(shurikenPrefab,firePoint.position, Quaternion.identity);
    }

    public  bool canAttack()
    {
        return _cooldownTimer > _attackCooldown && _playerController.CanUseShuriken();
    }

}
