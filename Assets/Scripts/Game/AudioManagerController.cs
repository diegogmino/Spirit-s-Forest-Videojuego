using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerController : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip bossMusic;
    public AudioClip deadSound;
    public AudioClip ambientMusic;
    public AudioClip winMusic;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusicBoss()
    {

        audioSource.Stop();
        audioSource.clip = bossMusic;
        audioSource.Play();

    }

    public void ChangeMusicDead()
    {
        audioSource.Stop();
        audioSource.clip = deadSound;
        audioSource.volume = 1f;
        audioSource.Play();
        Invoke("ChangeAmbientMusic", 7f);
    }

    private void ChangeAmbientMusic()
    {
        audioSource.Stop();
        audioSource.clip = ambientMusic;
        audioSource.Play();
    }

    public void ChangeMusicWin()
    {
        audioSource.Stop();
        audioSource.clip = winMusic;
        audioSource.volume = 1f;
        audioSource.Play();
        Invoke("ChangeAmbientMusic", 6f);
    }
}
