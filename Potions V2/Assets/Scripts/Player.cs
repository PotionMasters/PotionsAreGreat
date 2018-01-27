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

    [SyncVar] public string chatText;
    private ChatBox chat;


    public bool IsLocalHuman()
    {
        return isLocalPlayer;
    }

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        chat = FindObjectOfType<ChatBox>();
    }
    private void Start()
    {
        Role = id == 0 ? PlayerRole.Book : PlayerRole.Potion;

        gm.RegisterPlayer(this);
    }
    private void Update()
    {
        CmdChat(Input.inputString);
        chat.UpdateChat(chatText);
    }


    [Command]
    private void CmdChat(string text)
    {
        chatText += text;
    }
}
