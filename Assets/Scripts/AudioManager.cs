using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public gameManager gm;

    public AudioSource audioSource;

    public AudioSource playWinMusic;
    public AudioSource playLoseMusic;
    
    public AudioSource endHidingMusic;
    public AudioSource candadoSound;
    public AudioSource candySound;
    public AudioSource keySound;

    public AudioClip exploringMusic;
    public AudioClip chasignMusic;
    public AudioClip hidingMusic;

    public AudioClip winMusic;
    public AudioClip loseMusic;

    private AudioClip currentClip;

    private void Start()
    {
        audioSource.clip = exploringMusic;
        audioSource.loop = true;
        audioSource.Play();

    }

    public void playerWin(bool win)
    {
        if (win)
        {
            AudioClip newClip = null;
            newClip = winMusic;
            audioSource.Stop();
            audioSource.clip = currentClip;
            playWinMusic.Play();
        }
        else
        {
           // AudioClip newClip = null;
            //newClip = loseMusic;
            audioSource.Stop();
            //audioSource.clip = currentClip;
            playLoseMusic.Play();
        }
    }
    public void ChangeMusic(EnemyIA.EnemyState state)
    {
        AudioClip newClip = null;

        switch (state)
        {
            case EnemyIA.EnemyState.Patrol:
                newClip = exploringMusic;
                break;

            case EnemyIA.EnemyState.Chase:
                newClip = chasignMusic;
                break;

            case EnemyIA.EnemyState.Stalk:
                newClip = hidingMusic;
                break;
        }

        if (currentClip == newClip) return;


        currentClip = newClip;

        audioSource.Stop();
        audioSource.clip = currentClip;
        audioSource.loop = true;
        audioSource.Play();

    }
}
