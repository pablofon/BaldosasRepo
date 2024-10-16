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
    float currentGas;
    [SerializeField] TMP_Text gasText;
    
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
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
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

        gasText.text = currentGas.ToString();
    }
}
