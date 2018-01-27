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

    public string chatPhrase;
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
        chatPhrase += Input.inputString;
        if (chatPhrase.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            // Word complete - Send
            CmdChat(chatPhrase);
            chatPhrase = "";
        }

        // Update pre cb msg
        chat.textFieldMesh.text = chatPhrase;
    }


    [Command]
    private void CmdChat(string text)
    {
        RpcChat(text);
    }
    [ClientRpc]
    private void RpcChat(string text)
    {
        chat.AddMsg(text);
    }
}
