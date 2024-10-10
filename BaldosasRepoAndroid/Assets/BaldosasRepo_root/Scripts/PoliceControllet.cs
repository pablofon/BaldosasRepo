using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceControllet : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;

    [SerializeField] float nearestPos;
    [SerializeField] float furthestPos;
    [SerializeField] float currentPos;
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y,furthestPos);
        currentPos = furthestPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.chocado == true && currentPos != nearestPos)
        {
            currentPos = currentPos + 1;
        }
       
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, currentPos);

    }
}