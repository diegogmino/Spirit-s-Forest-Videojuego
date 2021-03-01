using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerController : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip bossMusic;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusicBoss(AudioClip music)
    {

        //StartCoroutine(FadeAudioSource.StartFade(audioSource, 3, 0, music));
        audioSource.Stop();
        audioSource.clip = music;
        audioSource.Play();

    }
}
