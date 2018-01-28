using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip mainTheme;
    public AudioClip menuTheme;
    public AudioClip player1Music;
    public AudioClip switchMusic;
    public AudioClip player2Music;


    void Start()
    {
        AudioManager.instance.PlayMusic(menuTheme, 3);
      
    }

    void Update()
    {
       
    

    }

    public void PlayerMusic(int amount)
    {

    }

   
}
