using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    PoliceControllet policeController;
    [SerializeField]Camera cam;
    Vector3 camOriginPos;
    Rigidbody rb;
    Animator animator;
    Section section;
    [SerializeField]ParticleSystem leftSparks;
    [SerializeField] ParticleSystem rightSparks;

    
    private Vector3 fp; //first touch pos
    private Vector3 lp; //last touch pos
    private float dragDistance; // distancia minima para swipe
    float impulseForce;
    [SerializeField] Transform[] positions;

    //[SerializeField] Transform leftPosition;
    //[SerializeField] Transform centerPosition;
    //[SerializeField] Transform rightPosition;
    //Vector3 currentPosition;
    private bool onLeft;
    private bool onRight;
    private Vector3 targetPosition;

    private bool isMoving;
    [SerializeField]public float lanesDistance;

    
    public bool chocado;
    public bool rampeado;
    public Component[] obstaclesInScene;


    // Start is called before the first frame update
    void Start()
    {
        policeController = FindObjectOfType<PoliceControllet>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        section = FindObjectOfType<Section>();
        camOriginPos = cam.transform.position;
        //targetPosition = positions[1].position;
        //transform.position = positions[1].position;
        //onLeft = false;
        //onRight = false;

        //targetPosition = centerPosition.position;
        impulseForce = 300f;
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
                            animator.SetTrigger("SoftRight");
                            
                            isMoving = true;
                            //targetPosition = transform.position + (Vector3.right * lanesDistance);
                            //transform.position = Vector3.Lerp(transform.position, targetPosition, 2f);

                            if (transform.position.x < 2.9f)
                            {
                                rb.AddForce(Vector3.right * impulseForce, ForceMode.Force);
                            }

                            //float dist = Vector3.Distance(transform.position, positions[2].position);
                            //if (dist < 0.01f) { transform.position = positions[2].position; }
                            //rb.AddForce(Vector3.right * 300, ForceMode.Force);

                            
                            /*if (transform.position == positions[0].position )
                            {
                                targetPosition = positions[1].position;
                                //transform.position = Vector3.Lerp(transform.position, targetPosition, 1f);
                               rb.AddForce(Vector3.right*300,ForceMode.Force);
                               
                            }
                            else if (transform.position == positions[1].position )
                            {
                                targetPosition = positions[2].position;
                                //transform.position = Vector3.Lerp(transform.position, targetPosition, 1f);
                                rb.AddForce(Vector3.right * 300, ForceMode.Force);


                            }*/



                        }
                        else
                        {

                            if (transform.position.x > -2.9f)
                            {
                                rb.AddForce(Vector3.left * impulseForce, ForceMode.Force);
                            }
                            //LEFT SWEEP
                            Debug.Log("Left");
                            animator.SetTrigger("SoftLeft");
                            //targetPosition = transform.position + (Vector3.left * lanesDistance);
                            //isMoving = true;
                            //transform.position = Vector3.Lerp(transform.position, targetPosition, 2f);
                            //rb.AddForce(Vector3.left * 300, ForceMode.Force);
                            /*if (transform.position == positions[2].position)
                            {
                                targetPosition = positions[1].position;
                                //transform.position = Vector3.Lerp(transform.position, targetPosition, 1f);
                                rb.AddForce(Vector3.left * 300, ForceMode.Force);
                            }
                            else if (transform.position == positions[1].position)
                            {
                                targetPosition = positions[0].position;
                                rb.AddForce(Vector3.left * 300, ForceMode.Force);
                                //transform.position = Vector3.Lerp(transform.position, targetPosition, 1f);
                            }*/
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
                    animator.SetTrigger("Tap");
                    rb.velocity = Vector3.zero;
                    return;                }
            }
        }

        cam.transform.position = new Vector3(0,transform.position.y+camOriginPos.y,camOriginPos.z); //La camara sigue al player en el eje y 
        
        if(chocado == true)
        {
            slowMo();
        }
    }

    void slowMo()
    {

      
        obstaclesInScene = FindObjectsOfType<Section>(); //Pilla todos los scripts Section de la escena
        for (int i = 0; i < obstaclesInScene.Length; i++)
        {
            GameObject obj = obstaclesInScene[i].gameObject;

            obj.GetComponent<Section>().maxSpeed = 30f; //Les baja la velocidad a todos 

            Invoke("EndSlowMo",1f); //Devuelve la velocidad
        }

    }

    void EndSlowMo()
    {
        chocado = false;
        for (int i = 0; i < obstaclesInScene.Length; i++)
        {
            GameObject obj = obstaclesInScene[i].gameObject;

            obj.GetComponent<Section>().maxSpeed = 40f;

            
        }
    }


    public void dead()
    {
        animator.SetTrigger("Dead");
        impulseForce = 0f;
        chocado = false;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ramp"))
        {
            rb.AddForce(Vector3.up * 4, ForceMode.Impulse);
            rampeado = true;
        }
        
        if (collision.gameObject.CompareTag("RightWall"))
        {
            rightSparks.gameObject.SetActive(true);

            chocado = true;

        }
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            leftSparks.gameObject.SetActive(true);
            chocado = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("RightWall"))
        {
            rightSparks.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            leftSparks.gameObject.SetActive(false);
        }
    }
}

    
      
        
       

