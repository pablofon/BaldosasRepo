using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroManager : MonoBehaviour
{
    public int obstacleNumber;
    public Section[] sections;
    public int currentSection;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GasPickSpawn();
    }

    public void GasPickSpawn()
    {
        if (GameManager.Instance.sectionsToGas == GameManager.Instance.gasSection)
        {
            
            sections[currentSection].gasPicks[currentSection].SetActive(true);
            GameManager.Instance.sectionsToGas = 0;
        }
    }
}
