using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /*
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("UIManager is null!");
            }
            return instance;
        }
    }*/

    [Header("UI References")]
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausePanel;
    float currentGas;
    [SerializeField] TMP_Text gasText;
    [SerializeField]bool isPaused = false;
    PlayerController playerController;
    
    /*
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }*/

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentGas = GameManager.Instance.gasolina;
        
        if (GameManager.Instance.gameCompleted)
        {
            winPanel.SetActive(true);
        }
        else
        {
            winPanel.SetActive(false);
        }

        if (GameManager.Instance.gameOver)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(false);
        }

        gasText.text = ((int)currentGas).ToString(); //Texto marcador
        if (GameManager.Instance.gasolina < 10) { gasText.color = Color.red; }
    }

    public void ShowPausePanel()
    {
        if (!isPaused)
        {
            pausePanel.SetActive(true);
            isPaused = true;
            playerController.impulseForce = 0f;
            Time.timeScale = 0f;

        }
        else
        {
            pausePanel.SetActive(false);
            isPaused = false;
            playerController.impulseForce = playerController.initImpulseForce;
            Time.timeScale = 1f;
        }
        
    }
    
}
