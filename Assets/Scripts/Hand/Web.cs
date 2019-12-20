using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;


using System.Threading;

public class Web:MonoBehaviour
{
	public static void CallToChildThread()
	{
		Debug.Log("Child thread starts");

		//建立udp服务器，参数2：udp协议以数据报的方式传输，参数3：UDP协议
		Socket udpServer=new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
		//为服务器绑定IP
		IPAddress ip=IPAddress.Parse("172.30.89.210");
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
		}

	}

	public static void init()
	{
		ThreadStart childref = new ThreadStart(CallToChildThread);
		Debug.Log("In Main: Creating the Child thread");
		Thread childThread = new Thread(childref);
		childThread.Start();
	}
}
