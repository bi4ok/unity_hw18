using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject playerEvent;
    [SerializeField] private Image heartImage;
    [SerializeField] private Sprite fullHP;
    [SerializeField] private Sprite halfHP;
    [SerializeField] private Sprite lowHP;

    private Text _currentHealthText;
    private Health _playerHealth;

    private void Start()
    {
        var interfaceControl = playerEvent.GetComponent<InterfaceController>();
        _playerHealth = interfaceControl.GetPlayerHealth();
        _currentHealthText = GetComponent<Text>();
        _currentHealthText.text = $"{_playerHealth.GetCurrentHealth()}";
    }

    private void Update()
    {
        var currentHealth = _playerHealth.GetCurrentHealth();
        _currentHealthText.text = $"{currentHealth}";
        if (currentHealth > 50)
        {
            heartImage.sprite = fullHP;
        }
        else if (currentHealth > 20)
        {
            heartImage.sprite = halfHP;
        }
        else if (currentHealth <= 0)
        {
            heartImage.sprite = lowHP;
        }
    }

}
