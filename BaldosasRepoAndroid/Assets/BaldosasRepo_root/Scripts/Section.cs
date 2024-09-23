using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    public List<GameObject> obstacles;
    [SerializeField] float speed;
    private int sectionCount;
    [SerializeField] float sectionSize;
    private static int lastRandomIndex = -1; //Es ESTÁTICA para poder compartir la variable entre todas las instancias de section y evitar que cada instancia tenga su variable lastRandomIndex
    
    // Start is called before the first frame update
    void Start()
    {
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

    public void EnableRandomObstacle()
    {
      foreach (GameObject obstacle in obstacles)
      {
            obstacle.SetActive(false); //Desactivamos todos los obstaculos

      }
        int randomIndex = lastRandomIndex; //definimos randomIndex como -1
        while (randomIndex == lastRandomIndex) //Se cumple la condicion la primera vez, pero la segunda vez ya ha calculado un nuevo randomIndex
        {
            randomIndex = Random.Range(0, obstacles.Count); //Numero Random entre 0 y num max de obstáculos
        }
        
        lastRandomIndex = randomIndex; //igualamos lastRandomIndex con randomIndex actual. Así cuando vuelva a pedir un randomIndex evita repetir (Repite el while hasta que da otro numero.)
        obstacles[randomIndex].SetActive(true);             //Activa uno aleatorio
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime); // Avanza el escenario hacia atras

        if(transform.position.z <= -sectionSize) //Si sale por detrás
        {
            transform.Translate(Vector3.forward * sectionSize * sectionCount); //La manda al final
            EnableRandomObstacle();
            GameManager.Instance.sections += 1; //El GameManager cuenta cuantas secciones se han superado
        }
    }

}
