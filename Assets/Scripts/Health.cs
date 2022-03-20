using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float _currentHealth;
    private bool _isAlive;
    private Animator _playerAnimator;


    private void Awake()
    {
        Debug.Log("PLAYER HEALTH AWAKE!!!");
        _playerAnimator = GetComponent<Animator>();
        _currentHealth = maxHealth;
        _isAlive = true;
        Debug.Log(_currentHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        CheckIsAlive();
        if (!_isAlive)
        {
            _playerAnimator.SetTrigger("Death");
        }

    }

    public bool CheckIsAlive()
    {
        _isAlive = _currentHealth > 0 ? true : false;
        return _isAlive;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
}
