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
    }

    public void GemReseter(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        GameManager.Instance.gameCompleted = false;
        GameManager.Instance.gameOver = false;
        GameManager.Instance.gasolina = GameManager.Instance.gasolinaInicial;
    }

    public void ExitGame()
    {
        Debug.Log("saliendo de Juego");
        Application.Quit();
    }
}
