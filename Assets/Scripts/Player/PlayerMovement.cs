using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField]    bool _isStayOnGround=true;

    private Animator _animator;
    private Rigidbody2D _player;
    private bool _isWatchingRight=true;
    private Vector2 _jumpDirection;


    void OnEnable()
    {
        _player = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _jumpDirection = new Vector2(0, _jumpPower);
    }

    void Update()
    {

        HorizontalMove(Input.GetAxis("Horizontal"));
        _animator.SetFloat("Speed",Mathf.Abs( _player.velocity.x));
        _animator.SetBool("isJump", !_isStayOnGround);

        if (Input.GetKeyDown(KeyCode.Space)&&_isStayOnGround)
        {
            Jump();
        }
    }

    private void HorizontalMove(float moveDirection)
    {
        _player.velocity = new Vector2(moveDirection * _maxSpeed,_player.velocity.y);        

        if (_isWatchingRight && moveDirection < 0 || !_isWatchingRight && moveDirection > 0)
        {
            TurnAround();
        }
    }

    private void Jump()
    {
        _player.AddForce(_jumpDirection);
        _isStayOnGround = false;
    }

    private void TurnAround()
    {
        _isWatchingRight = !_isWatchingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Ground>(out Ground ground))
        {
            _isStayOnGround = true;
        }
    }
}
