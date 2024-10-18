using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasolinePickUp : MonoBehaviour
{
    BoxCollider gasCol;
    AudioSource gasAudio;

    [Header("Gas Stats")]
    [SerializeField] float gasolinaSumada; //Cantidad de gasolina (tiempo) que recupera el player al conseguir el pickup
    [SerializeField] GameObject part;


    // Start is called before the first frame update
    void Start()
    {
        gasCol = GetComponent<BoxCollider>();
        gasAudio = GetComponent<AudioSource>();
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
            gasAudio.Play();
            Invoke("ShutDownParticles", 1.5f);

        }
    }

    private void ShutDownParticles()
    {
        part.SetActive(false);
    }
}
