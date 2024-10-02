using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Section section;
    
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
    public int sections = 0; //Secciones que el player ha pasado
    public int sectionsToGas;
    [SerializeField] int gasSection; //si sectionToGas = gasSection -> spawnea el pickup de gasolina
    [SerializeField] int totalSections; //Secciones que el jugador tiene que pasar para completar el nivel
    public bool sectionsCompleted = false;

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
        section = GetComponent<Section>();
        //Section.Instance.
    }

    private void Update()
    {
        if (gasolina <= 0f)
        {
            gameOver = true; 
        }

        GasDown();

        if (sections >= totalSections)
        {
            sectionsCompleted = true;
        }
        else
        {
            sectionsCompleted = false;
        }
    }

    void GasDown()
    {
        gasolina -= Time.deltaTime; 
    }

    void GasPickUpSpawn()
    {
        if (sectionsToGas == gasSection)
        {
            sectionsToGas = 0;
            //Acceder a la lista de Section y acceder al currentRandomIndex
            //Buscar en el hijo del obstaculo definido por randomIndex un objeto con el Tag PickUpSpawnPoint y spawnear un pickup en esa posicion
            int obstacleNumber;
            obstacleNumber = section.currentRandomIndex;
            //section.pickPositions[obstacleNumber];

        }
    }
}
