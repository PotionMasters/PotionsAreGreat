using UnityEngine;

public class MusicManager : MonoBehaviour {

    //public AudioClip mainTheme;
    public AudioClip menuTheme;
    public AudioClip player1Music;
    public AudioClip switchMusic;
    public AudioClip player2Music;
    public AudioClip endMusic;
    public float fadeTime = .5f;

    void Start()
    {
        AudioManager.instance.PlayMusic(menuTheme, fadeTime);
      
    }

    void Update()
    {
       
    

    }

   

   
}
