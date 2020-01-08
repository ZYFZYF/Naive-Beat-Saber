using System;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;



using System.Threading;
using Newtonsoft.Json.Linq;

using Newtonsoft.Json;
using System.Linq;


public class Web : MonoBehaviour
{
    private static float[,] hand;

    private static GameObject laser_left;
    private static GameObject laser_right;
    private static int flag = 0;

    private Text text;

    void Start()
    {
        text = GameObject.Find("WebMessage").GetComponent<Text>();
        //Debug.Log("In Main: Creating the Child thread");
        hand = new float[4, 3];
        //parseString ("{"Right hand": "{"Distal": "85.6001205444 99.5957107544 89.5335693359", "Proximal": "94.1328048706 123.874298096 69.6296234131"}", "Left hand": "{"Distal": "-57.0477333069 52.4687042236 109.270484924", "Proximal": "-69.1746292114 82.2418823242 107.529426575"}"}");
        ThreadStart childref = new ThreadStart(CallToChildThread);
        Thread childThread = new Thread(childref);
        laser_left = GameObject.Find("LaserSwordPrefab_Left");
        laser_right = GameObject.Find("LaserSwordPrefab_Right");
        laser_left.transform.position = new Vector3(-0.36f, 2f, 0.5f);
        laser_right.transform.position = new Vector3(0.36f, 2f, 0.5f);
        //float x=getAngle(new Vector3(-1,0,1),new Vector3(1,0,0));
        childThread.Start();
    }
    private static Vector3 GetVerticalDir(Vector3 _dir)
    {
        if (_dir.z == 0)
        {
            return new Vector3(0, 0, -1);
        }
        else
        {
            return new Vector3(-_dir.z / _dir.x, 0, 1).normalized;
        }
    }

    // unuse now
    private float getAngle(Vector3 a, Vector3 b)
    {
        Vector3 c = Vector3.Cross(a, b);
        float angle = Vector3.Angle(a, b);

        // a 到 b 的夹角
        float sign = Mathf.Sign(Vector3.Dot(c.normalized, Vector3.Cross(b, a)));
        float signed_angle = angle * sign;

        Debug.Log("b -> a :" + signed_angle);

        return signed_angle;
    }
    private void dealRotation(int index)
    {
        float x = hand[index, 0] - hand[1 + index, 0];
        float y = hand[index, 1] - hand[index + 1, 1];
        float z = hand[index, 2] - hand[index + 1, 2];

        Vector3 relativePos = new Vector3(x, y, z);
        // the second argument, upwards, defaults to Vector3.up
        if (index == 0)
            laser_left.transform.rotation = Quaternion.LookRotation(GetVerticalDir(relativePos), relativePos);
        else
            laser_right.transform.rotation = Quaternion.LookRotation(GetVerticalDir(relativePos), relativePos);
    }

    public void setLaserPos()
    {
        if (flag == 1)
        {
            ShowMessage("suceess to parse message");
            laser_left.transform.position = new Vector3(hand[1, 0], hand[1, 1], hand[1, 2]);
            laser_right.transform.position = new Vector3(hand[3, 0], hand[3, 1], hand[3, 2]);
            //Debug.Log(string.Format("left laser pos is ({0},{1},{2})\n", hand[0, 0], hand[0, 1], hand[0, 2]));
            //Debug.Log(string.Format("right laser pos is ({0},{1},{2})\n", hand[2, 0], hand[2, 1], hand[2, 2]));
            // 0-1 2-3
            dealRotation(0);
            dealRotation(2);
            //flag=0;
        }
        else ShowMessage("faild to parse message or not received");
    }

    private static void getFloat(int index, string input)
    {
        String[] sArray = input.Split(' ');
        for (int i = 0; i < 3; i++)
            hand[index, i] = Convert.ToSingle(sArray[i]) / 120f;
        //Debug.Log(string.Format("get float ({0}, {1}, {2})", hand[index, 0], hand[index, 1], hand[index, 2]));
    }
    private static void parseString(string message)
    {
        try
        {
            //Debug.Log("in parseString " + message);
            JObject jo = JObject.Parse(message);
            foreach (var x in jo)
            {
                //Debug.Log(x.Key + x.Value);
                //Console.WriteLine("{0} : {1}", x.Key, x.Value);
                string singlehand = x.Value.ToString();
                JObject singleObj = JObject.Parse(singlehand);
                if (x.Key == "Left hand")
                {
                    getFloat(0, singleObj["Distal"].ToString());
                    getFloat(1, singleObj["Proximal"].ToString());
                }
                else
                {
                    getFloat(2, singleObj["Distal"].ToString());
                    getFloat(3, singleObj["Proximal"].ToString());
                }
                // UpdateDebug.Log("parse succeed");
            }
            flag = 1;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }

    EndPoint ep;
    private void CallToChildThread()
    {
        Debug.Log("Child thread starts");

        //建立udp服务器，参数2：udp协议以数据报的方式传输，参数3：UDP协议
        Socket udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //为服务器绑定IP
        ep = new IPEndPoint(IPAddress.Any, 2233);
        udpServer.Bind(ep);
        //接收数据
        //EndPoint endP = new IPEndPoint(IPAddress.Any, 2233);
        string message;
        byte[] data = new byte[1024];
        int length = 0;

        //把数据的来源放到第二个参数上
        Debug.Log("before enter");
        while (true)
        {
            //Debug.Log("in true");
            length = udpServer.ReceiveFrom(data, ref ep);
            message = Encoding.UTF8.GetString(data, 0, length);
            //Debug.Log (endP.ToString () + message);
            parseString(message);
        }

    }
    //只能在主线程被调用
    void ShowMessage(String message)
    {
        text.text = message;
    }
}
