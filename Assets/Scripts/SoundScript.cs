using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public static SoundScript instance;

    public AudioSource[] musicSource;

    private AudioSource mainMenuMusic;
    private AudioSource playMusic;
    private AudioSource collectable;
    private AudioSource hit;
    private AudioSource buttonPress;

    void Awake(){
        mainMenuMusic = musicSource[0];
        playMusic = musicSource[1];
        collectable = musicSource[2];
        hit = musicSource[3];
        buttonPress = musicSource[4];

        mainMenuMusic.loop = true;
        playMusic.loop = true;
    }

    void Start(){
        instance = this;
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

    public void changeSFXVolume(float volume){
        collectable.volume = volume;
        hit.volume = volume;
        buttonPress.volume = volume;
    }

    public void playHit(){
        hit.Play();
    }

    public void playCollectable(){
        collectable.Play();
    }

    public void playButtonPress(){
        buttonPress.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
