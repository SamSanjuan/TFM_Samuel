using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraChange : MonoBehaviour
{
    public GameObject cameraAct;
    public GameObject cameraDes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraAct.SetActive(true);
            cameraDes.SetActive(false);
        }
    }
}
