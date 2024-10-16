using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightStrobe : MonoBehaviour
{
    [SerializeField] float maxIntensiy;
    Light luz;
    private bool ended;
    // Start is called before the first frame update
    void Start()
    {
        luz = gameObject.GetComponent<Light>();
        ended = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ended)
        {
            StartCoroutine(Stroboscopy());
        }

    }

    IEnumerator Stroboscopy()
    {
        ended = false;
        luz.intensity = 0;
        yield return new WaitForSeconds(.1f);
        luz.intensity = maxIntensiy;
        yield return new WaitForSeconds(.1f);
        ended = true;



    }
}
