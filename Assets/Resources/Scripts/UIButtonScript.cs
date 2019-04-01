using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonScript : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Elemento.hits = 3;
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        Elemento.crystals = 0;
        SceneManager.LoadScene(Elemento.level);
    }
}
