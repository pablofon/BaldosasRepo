using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    public List<GameObject> obstacles;
    public List<GameObject> gasPicks;
    public float speed;     //Velocidad definida en inspector
    public float maxSpeed;  
    private int sectionCount;
    [SerializeField] float sectionSize;
    private static int lastRandomIndex = -1; //Es ESTÁTICA para poder compartir la variable entre todas las instancias de section y evitar que cada instancia tenga su variable lastRandomIndex
    public int currentRandomIndex;
    [SerializeField] bool finishLineAppear = false;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = speed;
        sectionCount = GameObject.FindGameObjectsWithTag("Section").Length;
        obstacles = new List<GameObject>(); 
        
        foreach (Transform child in transform)
        {
            if (child.tag == "Obstacle")
            {
                obstacles.Add(child.gameObject);

            }   
        }

        EnableRandomObstacle();
    }

   

    void Update()
    {
        transform.Translate(Vector3.back * maxSpeed * Time.deltaTime); // Avanza el escenario hacia atras

        if(transform.position.z <= -sectionSize) //Si sale por detrás
        {
            transform.Translate(Vector3.forward * sectionSize * sectionCount); //La manda al final 
            GameManager.Instance.sections += 1; //El GameManager cuenta cuantas secciones se han superado
            GameManager.Instance.sectionsToGas += 1; //Tambien cuenta las secciones para que aparezca la gasolina
            EnableRandomObstacle();
        }
    }

    public void EnableRandomObstacle()
    {
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false); //Desactivamos todos los obstaculos

        }

        int randomIndex = lastRandomIndex; //definimos randomIndex como -1
        currentRandomIndex = randomIndex;
        while (randomIndex == lastRandomIndex) //Se cumple la condicion la primera vez, pero la segunda vez ya ha calculado un nuevo randomIndex
        {
            randomIndex = Random.Range(0, obstacles.Count - 1); //Numero Random entre 0 y num max de obstáculos-1 (el último es la meta)
        }
        lastRandomIndex = randomIndex; //igualamos lastRandomIndex con randomIndex actual. Así cuando vuelva a pedir un randomIndex evita repetir (Repite el while hasta que da otro numero.)
        currentRandomIndex = randomIndex;

        if (GameManager.Instance.sectionsCompleted == false)    //Si no se ha completado la carrera
        {
            
            
            obstacles[randomIndex].SetActive(true);             //Activa uno aleatorio si no se ha completado la carrera
        }
        else                                                    //Si se ha completado la carrera
        {
            if (!finishLineAppear)                              //Si aun no ha aparecido la meta
            {
                obstacles[obstacles.Count - 1].SetActive(true); //Activa la meta 
                finishLineAppear = true;                        //Evita volver a activarla
            }
            else                                                //SI ya ha aparecido la meta
            {
                obstacles[0].SetActive(true);
            }
            
            
        }

       

    }

}
