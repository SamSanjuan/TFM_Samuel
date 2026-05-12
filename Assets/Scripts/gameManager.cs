using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public bool canMove;
    public bool actAntagonista = true;

    [Header ("Condiciones Del Juego")]
    public int coins = 0;
    public bool hasKey = false;
    public bool playerIsHidden;

    public GameObject antagonista;
}
