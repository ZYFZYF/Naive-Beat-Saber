using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeShow : MonoBehaviour
{
    private Text text;
    private int minute;
    private int second;
    private int millisecond;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        float now_time = Time.timeSinceLevelLoad;
        minute = (int)(now_time / 60);
        second = (int)(now_time - minute * 60);
        millisecond = (int)((now_time - (int)now_time) * 1000);
        text.text = string.Format("{0:D2}:{1:D2}:{2:D2}", minute, second, millisecond);
    }
}
