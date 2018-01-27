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
    private string chatWord;
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
        chatWord += Input.inputString;
        if (chatWord.Length > 0 && chatWord[chatWord.Length - 1] == ' ')
        {
            // Word complete - Send
            CmdChat(chatWord);
            chatWord = "";
        }

        chat.UpdateChat(chatText + chatWord);
    }


    [Command]
    private void CmdChat(string text)
    {
        chatText += text;
    }
}
