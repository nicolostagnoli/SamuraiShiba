using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    public float activeTime;
    private float _timeActivated;
    private float alpha;

    public float alphaSet = 0.8f;
    private float _alphaMultiplier = 0.85f;

    private Transform _player;

    private SpriteRenderer _sr;
    private SpriteRenderer _playerSr;
    private Color color;


    private void OnEnable()
    {
        _sr = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerSr = _player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        _sr.sprite = _playerSr.sprite;
        transform.position = _player.position;
        transform.rotation = _player.rotation;
        _timeActivated = Time.time;
    }

    private void Update()
    {
        alpha *= _alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        _sr.color = color;

        if (Time.time >= (_timeActivated * activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }

}
