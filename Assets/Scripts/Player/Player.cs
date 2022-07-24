using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;    
    [SerializeField] private bool _isImmunityTurnOn;

    public UnityEvent<Enemy> Hit;
    public UnityEvent Dead;

    private Animator _animator;
    private int _isHitHash = Animator.StringToHash("isHit");
    private Vector2 _startPosition;
    private float _timeOfImmunity=3f;      
    private float _blinkingSpeed=5f;
    private Color _standartColor;
    private Coroutine _goBlinking;
    private Coroutine _changingAlpha;
    private Coroutine _turningOnImmunity;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _startPosition = transform.position;
        _currentHealth = _maxHealth;
        _standartColor = _renderer.color;
    }

    private void Update()
    {
        if (!_isImmunityTurnOn)
        {
            ReturnStandartColor();
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isImmunityTurnOn==false)
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {                
                _currentHealth--;
                Hit?.Invoke(enemy);
                _animator.SetTrigger(_isHitHash);

                if (_turningOnImmunity != null)
                {
                    StopCoroutine(_turningOnImmunity);
                }

                _turningOnImmunity = StartCoroutine(TurnOnImmunity());
            }
        }        
    }

    private void Die()
    {
        transform.position = _startPosition;
        _currentHealth = _maxHealth;
        Dead?.Invoke();
    }

    private void ReturnStandartColor()
    {
        _renderer.color = _standartColor;
    }

    private IEnumerator TurnOnImmunity()
    {
        
        _isImmunityTurnOn = true;
        float currentTime = 0f;

        if (_goBlinking != null)
        {
            StopCoroutine(_goBlinking);
        }

        _goBlinking=StartCoroutine(Blinking());

        while (currentTime <= _timeOfImmunity)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        _isImmunityTurnOn = false;
        StopCoroutine(_goBlinking);
        StopCoroutine(_changingAlpha);
    }

    private IEnumerator Blinking()
    {
        float minValue = 0f;
        float maxValue = 1f;
        

        while (_isImmunityTurnOn)
        {
            if (_renderer.color.a == minValue)
            {
                if (_changingAlpha != null) 
                {
                    StopCoroutine(_changingAlpha);
                }
                _changingAlpha = StartCoroutine(ChangeAlpha(maxValue));
            }

            if (_renderer.color.a == maxValue)
            {
                if (_changingAlpha != null)
                {
                    StopCoroutine(_changingAlpha);
                }
                _changingAlpha = StartCoroutine(ChangeAlpha(minValue));
            }

            yield return null;
        }        
    }

    private IEnumerator ChangeAlpha(float value)
    {
        float currentAlpha;

        while (_renderer.color.a != value)
        {
            currentAlpha = Mathf.MoveTowards(_renderer.color.a, value, Time.deltaTime *_blinkingSpeed );
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, currentAlpha);
            yield return null;
        }
    }
}

