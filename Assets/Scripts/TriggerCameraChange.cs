using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraChange : MonoBehaviour
{
    public GameObject cameraAct;
    public GameObject cameraDes;
    public gameManager gm;
    public bool canActAntognista = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraAct.SetActive(true);
            cameraDes.SetActive(false);
            Debug.Log("cambiocamara");
        }

        if (other.CompareTag("Player") && gm.actAntagonista && canActAntognista)
        {
            gm.antagonista.SetActive(true);
            canActAntognista = false;
        }
    }
}
