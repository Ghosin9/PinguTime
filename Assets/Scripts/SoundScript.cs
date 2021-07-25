using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public AudioSource[] musicSource;

    private AudioSource mainMenuMusic;
    private AudioSource playMusic;

    void Awake(){
        mainMenuMusic = musicSource[0];
        playMusic = musicSource[1];

        mainMenuMusic.loop = true;
        playMusic.loop = true;
    }

    public void playMainMenu(){
        mainMenuMusic.Play();
        playMusic.Stop();
    }

    public void playRegularMusic(){
        mainMenuMusic.Stop();
        playMusic.Play();
    }

    public void changeVolume(float volume){
        mainMenuMusic.volume = volume;
        playMusic.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
