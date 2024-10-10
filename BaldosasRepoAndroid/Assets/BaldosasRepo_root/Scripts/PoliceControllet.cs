using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoliceControllet : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;

    [SerializeField] float nearestPos;
    [SerializeField] float middlePos;
    [SerializeField] float furthestPos;
    [SerializeField] float[] positions;
    int i;
    [SerializeField] float currentPos;
    public Vector3 targetPos;
    public Vector3 velocity = new Vector3(1,1,1);
    public float initPos;
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, positions[2]);
        initPos = transform.position.z;
        currentPos = positions[2];
        i = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.chocado == true && currentPos != positions[0])
        {
            i -= 1;
            currentPos = positions[i];
            playerController.chocado = false;
        }
        if (playerController.rampeado)
        {
            i = 2;
            currentPos = positions[i];
        }

        if(transform.position.z < positions[2]+.01f)
        {
            i = 2;
            playerController.rampeado = false;
        }

        if (playerController.chocado == true && currentPos == positions[0])
        {

            playerController.dead();
            
        }

        targetPos = new Vector3(player.transform.position.x, player.transform.position.y, currentPos);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, .3f);

       
    }

    /*public void ComeBack()
    {
        //targetPos = Vector3.SmoothDamp(transform.position, initPos, ref velocity, .3f);
       
    }*/
}