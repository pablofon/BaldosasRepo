using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoliceControllet : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;

    [SerializeField] float nearestPos;
    [SerializeField] float furthestPos;
    [SerializeField] float currentPos;
    Vector3 targetPos;
    Vector3 velocity = new Vector3(1,1,1);
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
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
       
        targetPos = new Vector3(player.transform.position.x, player.transform.position.y, currentPos);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, .3f);
    }
}