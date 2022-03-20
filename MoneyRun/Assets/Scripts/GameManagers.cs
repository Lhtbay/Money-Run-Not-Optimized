using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagers : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    private Player_movement _playerMovement;
    private bool _isStart = false;
    public void Start()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_movement>();
    }

    private void Update()
    {
        if (!_isStart)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();            
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   public void StartGame()
    {
        _canvas.SetActive(false);
        _isStart = true;
        _playerMovement.enabled = true;

    }
}
