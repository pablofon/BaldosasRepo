using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Vector3 fp; //first touch pos
    private Vector3 lp; //last touch pos
    private float dragDistance; // distancia minima para swipe

    //Input inputSystem;

    private Vector3 targetPosition;
    private bool isMoving;
    [SerializeField]public float lanesDistance;


    // Start is called before the first frame update
    void Start()
    {
        //inputSystem = GetComponent<Input>();
        targetPosition = transform.position;
        isMoving = false;
        dragDistance = Screen.height * 15 / 100; //Distancia del sweep
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lp = touch.position;
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {
                        if (lp.x > fp.x)
                        {
                            //RIGHT SWEEP
                            Debug.Log("Right");
                            targetPosition = transform.position + (Vector3.right * lanesDistance);
                            //isMoving = true;
                            transform.position = Vector3.Lerp(transform.position, targetPosition, 2f);
                            
                        }
                        else
                        {
                            //LEFT SWEEP
                            Debug.Log("Left");
                            targetPosition = transform.position + (Vector3.left * lanesDistance);
                            //isMoving = true;
                            transform.position = Vector3.Lerp(transform.position, targetPosition, 2f);
                        }
                    }

                    else
                    {
                        if (lp.y > fp.y)
                        {
                            //UP SWEEP
                            Debug.Log("Up");
                        }
                        else
                        {
                            //DOWN SWEEP
                            Debug.Log("Down");
                        }
                    }

                }

                else
                {
                    //Es un tap
                    Debug.Log("Just a Tap");

                }
            }
        }
    }
}

    
      
        
       

