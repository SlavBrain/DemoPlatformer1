using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed=2;
    [SerializeField] private float _jumpPower=8;
    [SerializeField] float _powerMultiplyerToBounceBack = 5;

    [SerializeField] bool _isStayOnGround = true;



    private Player _player;
    private Animator _animator;
    private Rigidbody2D _rigidbody2d;
    private SpriteRenderer _spriteRenderer;

    private int speedHash = Animator.StringToHash("Speed");
    private int isJumpHash = Animator.StringToHash("isJump");

    private Vector2 _bounceDirection;    

    private bool _isWatchingRight = true;

    private Coroutine _lockingController;
    private float _lockingControllerTime=1f;
    private bool _isLockController;


    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _player.Hit.AddListener(BounceBack);        
    }
    
    private void Update()
    {        
        _animator.SetFloat(speedHash, Mathf.Abs(_rigidbody2d.velocity.x));
        _animator.SetBool(isJumpHash, !_isStayOnGround);

        if (_isLockController == false)
        {
            HorizontalMove(Input.GetAxis("Horizontal"));

            if (Input.GetKeyDown(KeyCode.Space) && _isStayOnGround)
            {
                Jump();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Ground>(out Ground ground))
        {
            _isStayOnGround = true;
        }
    }

    private void HorizontalMove(float moveDirection)
    {
        _rigidbody2d.velocity = new Vector2(moveDirection * _maxSpeed, _rigidbody2d.velocity.y);

        if (_isWatchingRight && moveDirection < 0 || !_isWatchingRight && moveDirection > 0)
        {
            TurnAround();
        }
    }

    private void Jump()
    {
        _rigidbody2d.velocity=new Vector2(_rigidbody2d.velocity.x, _jumpPower);
        _isStayOnGround = false;
    }

    private void TurnAround()
    {
        _isWatchingRight = !_isWatchingRight;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
        //Vector2 Scale = transform.localScale;
        //Scale.x *= -1;
        //transform.localScale = Scale;
    }    

    private void BounceBack(Enemy enemy)
    {
        _bounceDirection = (transform.position - enemy.transform.position).normalized;
        _rigidbody2d.velocity=_bounceDirection* _powerMultiplyerToBounceBack;
        _lockingController = StartCoroutine(LockController());
    }

    private IEnumerator LockController()
    {
        _isLockController = true;
        yield return new WaitForSeconds(_lockingControllerTime);
        _isLockController = false;
    }
}
