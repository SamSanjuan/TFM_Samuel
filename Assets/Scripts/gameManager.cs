using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class gameManager : MonoBehaviour
{
    public bool canMove;
    public bool actAntagonista = true;

    [Header ("Condiciones Del Juego")]
    public int coins = 0;
    public bool hasKey = false;
    public bool playerIsHidden;

    public GameObject antagonista;
    public GameObject player;
    public NavMeshAgent nma;
    public PlayerMovement pl;

    public GameObject particulas;
    public GameObject defeatPanel;
    public GameObject victoryPanel;

    public AudioManager am;
    public bool finishGame = false;
    public bool capFinishGame = false;
    private bool pause = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            pause = !pause;

            if (pause)Time.timeScale = 0f;
            else Time.timeScale = 1f;
        }
    }
    public void endGame(bool victory)
    {
        if (!capFinishGame)
        {
            if (victory)
            {
                finishGame = true;
                canMove = false;
                antagonista.SetActive(false);
                am.playerWin(true);
                victoryPanel.SetActive(true);
                pl.speed = 0;

            }
            else
            {
                finishGame = true;
                am.playerWin(false);
                particulas.SetActive(true);
                canMove = false;
                StartCoroutine(decreaseSpeed());
                StartCoroutine(showDefeatPanel());
                pl.speed = 0;
            }
            capFinishGame = true;
        }
    }

    private IEnumerator showDefeatPanel()
    {
        yield return new WaitForSeconds(3);
        defeatPanel.SetActive(true);
    }

    private IEnumerator decreaseSpeed()
    {
        nma.speed = 2;
        yield return new WaitForSeconds(1);
        nma.speed = 1;
        yield return new WaitForSeconds(.5f);
        nma.speed = 0;

    }
}

