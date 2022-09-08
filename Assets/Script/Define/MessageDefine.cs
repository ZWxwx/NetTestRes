using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MessageType
{
    None=0,
    System=1,
    Talk=2
}
public class MessageDefine
{

    public MessageType Type { get; set; }

    public string Sender { get; set; }
    public string Content { get; set; }

    public MessageDefine(MessageType type,string Sender ,string content){
        this.Type = type;
        this.Sender = Sender;
        this.Content = content;
    }
        
}
