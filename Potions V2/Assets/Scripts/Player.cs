using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum PlayerRole { Potion, Book }

public class Player : NetworkBehaviour
{
    [SyncVar] public short id;
    private GameManager gm;
    public PlayerRole Role { get; private set; }

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        Role = id == 0 ? PlayerRole.Book : PlayerRole.Potion;

        gm.RegisterPlayer(this);
    }

    public bool IsLocalHuman()
    {
        return isLocalPlayer;
    }
}
