using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Canvas deathCanvas;
    [SerializeField] private GameObject player;

    private Health _playerHealth;

    private void Start()
    {
        gameCanvas.gameObject.SetActive(true);
        deathCanvas.gameObject.SetActive(false);
        _playerHealth = player.GetComponent<Health>();
    }

    private void Update()
    {
        if (!_playerHealth.CheckIsAlive())
        {
            gameCanvas.gameObject.SetActive(false);
            deathCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
