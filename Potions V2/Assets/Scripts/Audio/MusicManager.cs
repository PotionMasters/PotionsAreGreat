﻿using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip mainTheme;
    public AudioClip menuTheme;

    void Start()
    {
        AudioManager.instance.PlayMusic(mainTheme, 3);
      
    }

    void Update()
    {
       
    

    }

   
}
