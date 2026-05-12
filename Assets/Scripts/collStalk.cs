using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collStalk : MonoBehaviour
{
    public gameManager gm;
    public EnemyIA eIA;
    public int actualPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            eIA.ChangeStalkState(actualPoint);
        }
    }
}
