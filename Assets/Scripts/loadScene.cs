using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public void loadNextScene(int number)
    {
        SceneManager.LoadScene(number);
        Debug.Log("sig escena");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
