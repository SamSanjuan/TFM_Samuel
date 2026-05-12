using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public void loadNextScene()
    {
        SceneManager.LoadScene(1);
        Debug.Log("sig escena");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
