using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Shooter))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Animator _playerAnimator;
    public float bonusScore { get; private set; } = 0;
    private bool _canMove = true;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_canMove)
        {
            MoveCharacter();
        }
    }

    private void MoveCharacter()
    {
        float horizontalDirection = Input.GetAxis(GlobalStringVars.horizontalAxis);
        bool isJumpButtonPressed = Input.GetButtonDown(GlobalStringVars.jump);

        if (Input.GetButtonDown(GlobalStringVars.fire_1))
        {
            _playerAnimator.SetTrigger("RangeAttack");
        }

        _playerMovement.Move(horizontalDirection, isJumpButtonPressed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            CointController coin = collision.GetComponent<CointController>();
            Animator coinState = collision.GetComponent<Animator>();
            if (!coinState.GetBool("Taken"))
            {
                bonusScore += coin.GetReward();
                coinState.SetBool("Taken", true);
            }

        }
    }

    public void CantMove()
    {
        _canMove = false;
    }

    public void CanMove()
    {
        _canMove = true;
    }

    public float CalculateRewardStep()
    {
        if (bonusScore > 0)
        {
            var deltaScore = bonusScore >= 5 ? 5 : bonusScore;
            bonusScore -= deltaScore;
            return deltaScore;
        }
        else
        {
            return 0;
        }

    }

}
