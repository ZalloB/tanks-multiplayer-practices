using UnityEngine;
using WebSocketSharp;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class WebSocketClient : MonoBehaviour {
	
	private WebSocket ws;
	public string sender = "UnityTest";
	public string location = "ws://localhost:8080/sample/chat/";
	public string chatroom = "battlefield";

    public bool joined;

	public Text board;
    public Text nickname;
    public Text message;

	public Queue<ChatMessageDto> messages = new Queue<ChatMessageDto>();

	void Start() {
        joined = false;
		ws = new WebSocket(location + chatroom);

		ws.OnOpen += OnOpenHandler;
		ws.OnMessage += OnMessageHandler;
		ws.OnClose += OnCloseHandler;

		ws.ConnectAsync();        

	}

	void Update() {

        if (!joined)
            return;
		
		while (messages.Count > 0) {
			ChatMessageDto msg = messages.Dequeue();
			string dateAndTime = String.Format("{0:dd/MM/yyyy, HH:mm:ss}", msg.getDateTime ());
			string message = dateAndTime + " " + msg.sender + "  " + msg.message;
			board.text = message+"\n" + board.text;
		}

	}

	public void Ping() {

        if (!joined)
            return;

        ChatMessageDto msg = new ChatMessageDto ();
		msg.sender = sender;
		msg.message = "Ping";
		string msgJson = JsonUtility.ToJson(msg);

		ws.SendAsync(msgJson, OnSendComplete);

		Debug.Log ("Ping: "+msg.sender+" : "+msg.message);

	}

	public void SendText() {

		ChatMessageDto msg = new ChatMessageDto ();
        msg.sender = nickname.text;
        msg.message = message.text;
        string msgJson = JsonUtility.ToJson(msg);

		ws.SendAsync(msgJson, OnSendComplete);

		Debug.Log ("SendMsg: "+msg.sender+" : "+msg.message);

	}

	void Close() {
		Debug.Log("WebSocket is closing");
		ws.CloseAsync();
	}

	void OnOpenHandler(object sender, System.EventArgs e) {
        joined = true;
        Debug.Log("WebSocket connected!");
	}

	void OnMessageHandler(object sender, MessageEventArgs e) {
        joined = true;
		string header = e.Data.Substring (2,"history".Length);
		Debug.Log("WebSocket server said: "+header);

		if (header.Equals("history")) {
			//Debug.Log("Adding History messages");
			ChatHistoryDto hist = JsonUtility.FromJson<ChatHistoryDto>(e.Data);
			foreach (ChatMessageDto msg in hist.history) {
				messages.Enqueue(msg);
			}
		}
		else {
			//Debug.Log("New message arrived");
			ChatMessageDto msg = JsonUtility.FromJson<ChatMessageDto>(e.Data);
			messages.Enqueue(msg);
		}
	}

	void OnCloseHandler(object sender, CloseEventArgs e) {
        joined = false;
        Debug.Log(e);
        Debug.Log("WebSocket closed with reason: " + e.Reason);
	}

	private void OnSendComplete(bool success) {
		Debug.Log("Message sent successfully? " + success);
	}

    private string DecorateColor(String user, string msg)
    {
        return "<color=" + GetColor(user) +">" + msg + "</color>" ;
    }

    private string GetColor(string code)
    {
        int hash = 0;

        for (int i = 0; i < code.Length; i++)
        {
            hash = code[i] + ((hash << 5) - hash);
        }

        string colour = "#";

        for (int i = 0; i < 3; i++)
        {
            int value = ((hash >> (i * 8)) & 0xff);
            string hex = "00" + value.ToString("X");
            colour += hex.Substring(hex.Length-2);
        }

        return colour;
    }
}

[Serializable]
public class ChatMessageDto {
	
	public string sender;
	public string message;
	public string received;

	public DateTime getDateTime() {
		return Convert.ToDateTime(received);
	}
}

[Serializable]
public class ChatHistoryDto {

	public ChatMessageDto[] history;
}