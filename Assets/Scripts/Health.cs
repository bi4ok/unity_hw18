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
        _playerAnimator = GetComponent<Animator>();
        _currentHealth = maxHealth;
        _isAlive = true;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        CheckIsAlive();
        Debug.Log(gameObject.name);
        Debug.Log(_currentHealth);
        Debug.Log(_isAlive);
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
