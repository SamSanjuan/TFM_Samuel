using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public gameManager gm;
    private bool capTrigger = false;
    public GameObject candadoImg;
    public GameObject openImg;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !capTrigger)
        {
            candadoImg.SetActive(true);
        }

        if (other.CompareTag("Player") && !capTrigger && gm.hasKey)
        {
            candadoImg.SetActive(false);
            StartCoroutine(transitions());
            capTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !capTrigger)
        {
            candadoImg.SetActive(false);
        }
    }

    private IEnumerator transitions()
    {
        openImg.SetActive(true);
        yield return new WaitForSeconds(1);
        // cargar siguiente escena
    }
}
