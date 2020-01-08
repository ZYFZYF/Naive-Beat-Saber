using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int comboCount;
    public int totalScore;
    private Text comboShow;
    private Text scoreShow;
    // Start is called before the first frame update
    void Start()
    {
        comboShow = GameObject.Find("TimeShow").GetComponent<Text>();
        scoreShow = GameObject.Find("WebMessage").GetComponent<Text>();
        comboCount = 0;
        totalScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        comboShow.text = "COMBO\n " + comboCount;
        scoreShow.text = "SCORE\n " + totalScore;
    }
}
