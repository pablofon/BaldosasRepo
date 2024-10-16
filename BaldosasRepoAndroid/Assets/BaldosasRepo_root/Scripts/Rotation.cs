using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = .7f;
    }

    // Update is called once per frame
    void Update()
    {
       transform.Rotate(0,0,rotationSpeed);
    }
}
