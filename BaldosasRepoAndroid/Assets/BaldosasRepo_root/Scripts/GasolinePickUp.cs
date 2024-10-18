using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasolinePickUp : MonoBehaviour
{
    BoxCollider gasCol;

    [Header("Gas Stats")]
    [SerializeField] float gasolinaSumada; //Cantidad de gasolina (tiempo) que recupera el player al conseguir el pickup
    [SerializeField] GameObject part;

    // Start is called before the first frame update
    void Start()
    {
        gasCol = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.gasolina += gasolinaSumada;
            part.SetActive(false);
            part.SetActive(true);
            Invoke("ShutDownParticles", 1.5f);

        }
    }

    private void ShutDownParticles()
    {
        part.SetActive(false);
    }
}
