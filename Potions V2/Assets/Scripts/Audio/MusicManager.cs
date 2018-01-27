using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    [Tooltip("Spacebar initiates the main theme ")]
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
