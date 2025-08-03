using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Lerp")]
    public float lerpSpeed = 1f;
    public Transform target;

    public bool invincible = false;
    public float speed = 1f;
    public string tagEnemy = "Enemy";
    public string tagEndLine = "EndLine";
    public GameObject endScreen;
    public TextMeshPro uiTextPowerUp;

    private bool _canRun;
    private float _currentSpeed;
    private Vector3 _pos;
    private Vector3 _startPosition;

    public void StartRun()
    {
        _canRun = true;
    }

    private void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }

    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }

    private void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagEnemy)
        {
            if(!invincible) EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagEndLine)
        {
            if(!invincible) EndGame();
        }
    }

    #region POWER-UPS

    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvincible(bool b)
    {
        invincible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;
    }

    public void ResetHeight()
    {
        var p = transform.position;
        p.y = _startPosition.y;
        transform.position = p;
    }
    #endregion
}
