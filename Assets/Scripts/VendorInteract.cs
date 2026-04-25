using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorInteract : MonoBehaviour
{
    public gameManager gm;
    private bool capTrigger = false;
    public GameObject vendorImg;
    public GameObject keyImg;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !capTrigger)
        {
            vendorImg.SetActive(true);
        }

        if (other.CompareTag("Player") && !capTrigger && gm.coins == 3)
        {
            vendorImg.SetActive(false);
            StartCoroutine(transitions());
            gm.hasKey = true;
            capTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !capTrigger)
        {
            vendorImg.SetActive(false);
        }
    }

    private IEnumerator transitions()
    {
        keyImg.SetActive(true);
        yield return new WaitForSeconds(2);
        keyImg.SetActive(false);
    }
}
