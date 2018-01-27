using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    private TextMesh chat_mesh;
    private string allText;


    public void UpdateChat(string allText)
    {
        this.allText = allText;
    }

    private void Awake()
    {
        chat_mesh = FindObjectOfType<TextMesh>();
    }
    private void Update()
    {
        chat_mesh.text = allText;
    }
}
