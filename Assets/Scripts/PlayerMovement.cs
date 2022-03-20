using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement vars")]
    [SerializeField, Range(0, 10)] private float jumpForce;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float speed;

    [Header("Settings")]
    [SerializeField] private Transform groundColliderTransform;
    [SerializeField] private float jumpOffset;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Camera cameraOnPlayer;

    private Animator _playerAnimator;
    private Rigidbody2D _playerRBody;
    private SpriteRenderer _playerSprite;
   
    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerRBody = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()
    {
        Vector3 overlapCirclePosition = groundColliderTransform.position;
        isGrounded = Physics2D.OverlapCircle(overlapCirclePosition, jumpOffset, groundMask);

    }

    public void Move(float direction, bool isJumpButtonPressed)
    {
        if (isJumpButtonPressed)
        {
            Jump();
        }

        if (Mathf.Abs(direction) > 0.01f)
        {
            HorizontalMovement(direction);
        }

        _playerAnimator.SetFloat("Speed", Mathf.Abs(_playerRBody.velocity[0]));
        if (_playerRBody.velocity[0] > 0)
        {
            _playerSprite.flipX = false;
        }
        else if (_playerRBody.velocity[0] < 0)
        {
            _playerSprite.flipX = true;
        }

        _playerAnimator.SetBool("Fall", !isGrounded);
     
    }

    private void Jump()
    {
        if (isGrounded)
        {
            _playerAnimator.SetTrigger("Jump");
            _playerRBody.velocity = new Vector2(_playerRBody.velocity.x, jumpForce);
        }
        
    }

    private void HorizontalMovement(float direction)
    {
        _playerRBody.velocity = new Vector2(curve.Evaluate(direction) * speed, _playerRBody.velocity.y);
    }
}
