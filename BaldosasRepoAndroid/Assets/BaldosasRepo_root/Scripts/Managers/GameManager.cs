using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Material curvedMat;
    //[SerializeField] Material curvedGasMat;
    float minCurve = -0.002f;
    float maxCurve = 0.002f;
    [SerializeField] float currentCurve = 0f;
    [SerializeField] bool toRightCurve = true;

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
    public int gasSection; //si sectionToGas = gasSection -> spawnea el pickup de gasolina
    public int totalSections; //Secciones que el jugador tiene que pasar para completar el nivel
    public bool sectionsCompleted = false;

    [Header("Game Stats")]
    public int coins; //Monedas
    public float gasolina; //Gasolina (tiempo) que le queda al player
    public float gasolinaInicial;


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
        curvedMat.SetFloat("_SidewaysStrenght", currentCurve);
        //curvedGasMat.SetFloat("_SidewaysStrenght", currentCurve);
        
        sectionsCompleted = false;

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
            //Aqu� activa la Meta
        }
        else
        {
            sectionsCompleted = false;
        }

        GasPickUpSpawn();
        EnvironmentCurvature();

        if (gameCompleted) { gameOver = false; }
        if (gameOver) { gameCompleted = false; }


    }

    void EnvironmentCurvature()
    {
        if (toRightCurve)
        {
            currentCurve += 0.0000005f;
            curvedMat.SetFloat("_SidewaysStrenght", currentCurve);
            //curvedGasMat.SetFloat("_SidewaysStrenght", currentCurve);
            
            if (currentCurve >= maxCurve)
            {
                toRightCurve = false;
                minCurve = Random.Range(0f,-.002f);
            }
        }
        else if (!toRightCurve)
        {
            currentCurve -= 0.0000005f;
            curvedMat.SetFloat("_SidewaysStrenght", currentCurve);
            //curvedGasMat.SetFloat("_SidewaysStrenght", currentCurve);
            if (currentCurve <= minCurve)
            {
                toRightCurve = true;
                maxCurve = Random.Range(0f, .002f);
            }
        }
        
        

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
            section.gasPicks[obstacleNumber].SetActive(true);
            sectionsToGas = 0;




        }
    }
}
