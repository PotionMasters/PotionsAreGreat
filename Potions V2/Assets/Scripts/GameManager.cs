using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public SeedManager seeder;

    [SerializeField] private GameObject PotionScene;
    [SerializeField] private GameObject RecipeScene;


    public Player[] Players { get; private set; }
    public Player BookPlayer { get; private set; }
    public Player PotionPlayer { get; private set; }
    public Player LocalHuman { get; private set; }

    public bool IsPlaying { get; private set; }
    public bool PlayersReady { get; private set; }
    private System.Action onPlayersReady;

    public void RegisterPlayer(Player player)
    {
        Players[player.id] = player;
        if (player.Role == PlayerRole.Book)
        {
            BookPlayer = player;
            Debug.Log("Registered book player (player " + player.id + ")");
        }
        else
        {
            PotionPlayer = player;
            Debug.Log("Registered potion player (player " + player.id + ")");
        }

        if (BookPlayer != null && PotionPlayer != null)
        {
            OnAllPlayersRegistered();
        }
    }
    public void DoOncePlayersReady(System.Action action)
    {
        Debug.Log("All players Ready");
        if (PlayersReady) action();
        onPlayersReady += action;

        if (BookPlayer.IsLocalHuman())
        {
            RecipeScene.SetActive(true);
        }
        else if (PotionPlayer.IsLocalHuman())
        {
            PotionScene.SetActive(true);
        }


    }

    private void Awake()
    {
        Players = new Player[2];
        IsPlaying = false;

        PotionScene.SetActive(false);
        RecipeScene.SetActive(false);

        //seeder.onSeedSet += (int seed) => // setup
    }
    private void OnAllPlayersRegistered()
    {
        // Set Ready
        PlayersReady = true;
        IsPlaying = true;

        // Event
        if (onPlayersReady != null)
            onPlayersReady();
    }
}
