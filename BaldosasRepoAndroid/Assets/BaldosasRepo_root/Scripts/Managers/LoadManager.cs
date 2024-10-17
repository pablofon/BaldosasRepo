using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public void SceneLoader(int sceneToLoad)
    {
        //GameManager.Instance.GameResetStatus(); //Crear en el GameManager si es necesario
        SceneManager.LoadScene(sceneToLoad);
        GameManager.Instance.gameCompleted = false;
        GameManager.Instance.gameOver = false;
        GameManager.Instance.sectionsCompleted = false;
        GameManager.Instance.sections = 0;
        GameManager.Instance.gasolina = GameManager.Instance.gasolinaInicial;
        GameManager.Instance.totalSections += 10;
        GameManager.Instance.gasSection += 1;
    }

    public void GemReseter(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        GameManager.Instance.gameCompleted = false;
        GameManager.Instance.gameOver = false;
        GameManager.Instance.gasolina = GameManager.Instance.gasolinaInicial;
        GameManager.Instance.sections = 0;
        GameManager.Instance.sectionsCompleted = false;
    }

    public void ExitGame()
    {
        Debug.Log("saliendo de Juego");
        Application.Quit();
    }
}
