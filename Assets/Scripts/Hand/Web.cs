using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;


using System.Threading;
using Newtonsoft.Json.Linq;


public class Web:MonoBehaviour
{
	private static Web instance=null;
	public static float[,] hand;
	private Web()
	{
		Debug.Log("In Main: Creating the Child thread");
		hand = new float[4, 3]; 
		//parseString ("{"Right hand": "{"Distal": "85.6001205444 99.5957107544 89.5335693359", "Proximal": "94.1328048706 123.874298096 69.6296234131"}", "Left hand": "{"Distal": "-57.0477333069 52.4687042236 109.270484924", "Proximal": "-69.1746292114 82.2418823242 107.529426575"}"}");
		ThreadStart childref = new ThreadStart(CallToChildThread);
		Thread childThread = new Thread(childref);
		childThread.Start();
	}

	public static Web getInstance()
	{
		if(instance==null)
			instance=new Web();
		return instance;
	}

	private static void getFloat(int index,string input)
	{
		String[] sArray = input.Split(' ');
		for (int i = 0; i < 3; i++)
			hand [index,i] = Convert.ToSingle (sArray[i]);

	}
	private static void parseString(string message)
	{
		try
		{
			Debug.Log ("in parseString");
			JObject jo = JObject.Parse(message);
			string left = jo["Left hand"].ToString();
			JObject leftHand=JObject.Parse(left);

			JObject rightHand = JObject.Parse (jo ["Right hand"].ToString ());
			getFloat (0, leftHand ["Distal"].ToString ());
			getFloat (1, leftHand ["Proximal"].ToString ());
			getFloat (2, rightHand ["Distal"].ToString ());
			getFloat (3, rightHand ["Proximal"].ToString ());
			Debug.Log(hand[0,0]);
			Debug.Log (hand [3,0]);
		}
		catch(Exception e)
		{
			Debug.Log(e);
		}
	}

	private void CallToChildThread()
	{
		Debug.Log("Child thread starts");

		//建立udp服务器，参数2：udp协议以数据报的方式传输，参数3：UDP协议
		Socket udpServer=new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
		//为服务器绑定IP
		IPAddress ip=IPAddress.Parse("127.0.0.1");
		EndPoint ep=new IPEndPoint(ip,2345);
		udpServer.Bind(ep);
		//接收数据
		EndPoint endP=new IPEndPoint(IPAddress.Any,0);
		string message;
		byte[] data=new byte[1024];
		int length=0;

		//把数据的来源放到第二个参数上
		Debug.Log("before enter");
		while (true) 
		{
			Debug.Log ("in true");
			length = udpServer.ReceiveFrom (data, ref endP);
			message = Encoding.UTF8.GetString (data, 0, length);
			Debug.Log (endP.ToString () + message);
			parseString (message);
		}

	}
}
