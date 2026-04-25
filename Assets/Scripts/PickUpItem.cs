using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public gameManager gm;
    private bool capTrigger = false;
    public GameObject coinImg;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !capTrigger)
        {
            gm.coins++;
            StartCoroutine(transitions());
            capTrigger = true;
        }
    }

    private IEnumerator transitions()
    {
        coinImg.SetActive(true);
        yield return new WaitForSeconds(2);
        coinImg.SetActive(false);
    }
}
