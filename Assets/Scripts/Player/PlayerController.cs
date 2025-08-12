using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    public bool invincible = false;
    public float speed = 1f;
    public string tagEnemy = "Enemy";
    public string tagEndLine = "EndLine";
    public GameObject endScreen;
    public TextMeshPro uiTextPowerUp;

    [Header("Lerp")]
    public float lerpSpeed = 1f;
    public Transform target;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    private bool _canRun;
    private float _currentSpeed;
    private float _baseSpeedOfAnimation = 5f;
    private Vector3 _pos;
    private Vector3 _startPosition;
    [SerializeField] private BounceHelper _bounceHelper;

    #region MÉTODOS PÚBLICOS
    public void StartRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedOfAnimation);
    }

    public void Bounce()
    {
        if(_bounceHelper != null) _bounceHelper.Bounce();
    }
    #endregion

    #region MÉTODOS PRIVADOS
    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
    }

    private void MoveBack(Transform t)
    {
        t.DOMoveZ(1.25f, .75f).SetRelative().SetEase(Ease.OutCubic);
    }

    private void StartScale()
    {
        this.transform.localScale = Vector3.zero;
        this.transform.DOScale(Vector3.one, .75f).SetEase(Ease.OutBack);
    }
    #endregion

    #region MÉTODOS UNITY
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagEnemy)
        {
            if (!invincible) 
            { 
                EndGame(AnimatorManager.AnimationType.DEATH);
                MoveBack(collision.transform);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagEndLine)
        {
            if(!invincible) EndGame(AnimatorManager.AnimationType.IDLE);
        }
    }

    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
        StartScale();
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
    #endregion

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

    public void ChangeHeight(float amount, float animationDuration)
    {
        /* var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p; */

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(Ease.OutBack);
    }

    public void ResetHeight(float animationDuration, Ease ease)
    {
        /* var p = transform.position;
        p.y = _startPosition.y;
        transform.position = p; */ 

        transform.DOMoveY(_startPosition.y, animationDuration).SetEase(ease);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion
}
