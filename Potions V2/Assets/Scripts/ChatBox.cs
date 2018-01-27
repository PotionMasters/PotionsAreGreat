using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    public TextMesh textFieldMesh;
    private GameManager gm;

    public CrystalBallMsg cbMsgPrefab;

    const int maxMsgs = 4;
    private LinkedList<ChatMsg> msgs = new LinkedList<ChatMsg>();


    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void AddMsg(string text)
    {
        ChatMsg msg = new ChatMsg();
        msg.text = text;
        msgs.AddLast(msg);
    }

    private void Update()
    {
        int count = 0;

        while (msgs.First != null && msgs.First.Value.active && msgs.First.Value.cbMsg == null)
        {
            // Expire msg
            msgs.RemoveFirst();
        }

        foreach (ChatMsg msg in msgs)
        {
            if (msg.active)
            {
                if (msg.cbMsg)
                {
                    // Still displaying
                    ++count;
                }
            }
            else if (msg.cbMsg == null)
            {
                // Display new msg
                msg.active = true;
                msg.cbMsg = Instantiate(cbMsgPrefab);
                msg.cbMsg.mesh.text = msg.text;
                msg.cbMsg.transform.SetParent(transform);
                ++count;
            }
            if (count >= maxMsgs)
            {
                break;
            }
        }
    }
}


class ChatMsg
{
    public string text = "";
    public CrystalBallMsg cbMsg;
    public bool active = false;
}