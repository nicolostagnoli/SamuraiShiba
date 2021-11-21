using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenAttack : MonoBehaviour
{

    [SerializeField]
    private float _attackCooldown;
    //animazione del personaggio che lancia lo shuriken
    private Animator _animator;
    private PlayerController _playerController;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject[] shurikens;
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
        shurikens[FindShuriken()].transform.position = firePoint.position;
        shurikens[FindShuriken()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localRotation.y));
    }

    public  bool canAttack()
    {
        return _cooldownTimer > _attackCooldown && _playerController.CanUseShuriken();
    }

    
    private int FindShuriken()
    {
        for (int i = 0; i < shurikens.Length; i++)
        {
            if (!shurikens[i].activeSelf)
                return i;
        }

        return 0;
    }
    
}
