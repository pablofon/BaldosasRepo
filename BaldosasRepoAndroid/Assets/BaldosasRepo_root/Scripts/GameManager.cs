using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("GameManager is null!");
            }
            return instance;
        }
    }

    [Header("Game Status")]
    public bool gameCompleted = false;
    public bool gameOver = false;
    public int sections = 0;

    [Header("Game Stats")]
    public int coins; //Monedas
    public float gasolina; //Gasolina (tiempo) que le queda al player


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

    }

    private void Start()
    {
        gameCompleted = false;
        gameOver = false;
    }

    private void Update()
    {
        if (gasolina <= 0f)
        {
            gameOver = true; 
        }

        GasDown();
    }

    void GasDown()
    {
        gasolina -= Time.deltaTime; 
    }
}
