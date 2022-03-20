using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class InterfaceController : MonoBehaviour
{
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Canvas deathCanvas;
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private Canvas cutSceneCanvas;
    [SerializeField] private CinemachineVirtualCamera gameplayCam;
    [SerializeField] private GameObject player;

    [SerializeField] private CinemachineVirtualCamera cutSceneCam;
    [SerializeField] private List<string> textForCutScene;
    [SerializeField] private Image imageWithText;

    private Health _playerHealth;
    private PlayerInput _playerObject;
    private RectTransform[] _cutScenePanels;
    private List<Vector3> _cutScenePanelsPosition = new List<Vector3>();
    private Text _textOnImageForCutScene;
    private bool _cutScenePlayed=false;

    private void Awake()
    {
        if (cutSceneCanvas && cutSceneCam && imageWithText)
        {
            cutSceneCam.Priority = 0;
            imageWithText.gameObject.SetActive(false);
            _textOnImageForCutScene = imageWithText.GetComponentInChildren<Text>();
            _cutScenePanels = cutSceneCanvas.GetComponentsInChildren<RectTransform>();
            _cutScenePanelsPosition.Add(_cutScenePanels[1].position + new Vector3(0, 200, 0));
            _cutScenePanelsPosition.Add(_cutScenePanels[2].position + new Vector3(0, -200, 0));
        }

        gameplayCam.Priority = 1;
        gameCanvas.gameObject.SetActive(true);
        deathCanvas.gameObject.SetActive(false);
        winCanvas.gameObject.SetActive(false);
        
        _playerHealth = player.GetComponent<Health>();
        _playerObject = player.GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        if (!_playerHealth.CheckIsAlive() && Time.timeScale != 0)
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(9) && !_cutScenePlayed)
        {
            _cutScenePlayed = true;
            StartCoroutine(CutScenePlay());
        }
    }

    private IEnumerator CutScenePlay()
    {
        gameplayCam.Priority = 0;
        cutSceneCam.Priority = 1;
        gameCanvas.gameObject.SetActive(false);
        cutSceneCanvas.gameObject.SetActive(true);
        _playerObject.CantMove();


        while (Vector3.Distance(_cutScenePanels[1].transform.position, _cutScenePanelsPosition[0]) > 10)
        {
            _cutScenePanels[1].transform.position = Vector3.MoveTowards(_cutScenePanels[1].position, _cutScenePanelsPosition[0], 2);
            _cutScenePanels[2].transform.position = Vector3.MoveTowards(_cutScenePanels[2].position, _cutScenePanelsPosition[1], 2);
            yield return new WaitForSeconds(Time.deltaTime * 2);
        }

        yield return new WaitForSeconds(1);
        imageWithText.gameObject.SetActive(true);


        foreach (string phrase in textForCutScene)
        {
            _textOnImageForCutScene.text = "";
            foreach (char letter in phrase)
            {
                _textOnImageForCutScene.text += letter;
                yield return new WaitForSeconds(Time.deltaTime * 10);
            }
            yield return new WaitWhile(() => !Input.GetButtonDown(GlobalStringVars.fire_1));

        }
        gameplayCam.Priority = 1;
        cutSceneCam.Priority = 0;
        gameCanvas.gameObject.SetActive(true);
        cutSceneCanvas.gameObject.SetActive(false);
        _playerObject.CanMove();

    }

    private void Death()
    {
        gameCanvas.gameObject.SetActive(false);
        deathCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public Health GetPlayerHealth()
    {
        return _playerHealth;
    }

    public PlayerInput GetPlayerController()
    {
        return _playerObject;
    }
}
