using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Shooter))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float horizontalDirection = Input.GetAxis(GlobalStringVars.horizontalAxis);
        bool isJumpButtonPressed = Input.GetButtonDown(GlobalStringVars.jump);

        if (Input.GetButtonDown(GlobalStringVars.fire_1))
        {
            _playerAnimator.SetTrigger("RangeAttack");
        }

        _playerMovement.Move(horizontalDirection, isJumpButtonPressed);
    }


}
