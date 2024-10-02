using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Section section;
    public int obstacleNumber;
    Vector3 pickupSpawnPos;
    [SerializeField] GameObject gas;
    
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
        section = FindObjectOfType<Section>();
        
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

        GasPickUpSpawn();
    }

    void GasDown()
    {
        gasolina -= Time.deltaTime; 
    }

    public void GasPickUpSpawn()
    {
        if (sectionsToGas == gasSection)
        {
            
            //Acceder a la lista de Section y acceder al currentRandomIndex
            //Buscar en el hijo del obstaculo definido por randomIndex un objeto con el Tag PickUpSpawnPoint y spawnear un pickup en esa posicion
            
            obstacleNumber = section.currentRandomIndex;
            Debug.Log("obstacleNum" + obstacleNumber);

            //pickupSpawnPos = section.gasPicks[section.currentRandomIndex].position;
            section.gasPicks[obstacleNumber].SetActive(true);
            
            //Instantiate(gas,pickupSpawnPos, Quaternion.identity);
            sectionsToGas = 0;




        }
    }
}
