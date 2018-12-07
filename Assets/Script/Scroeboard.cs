using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroeboard : MonoBehaviour {

    public Image[] _Player = new Image[4];  // 積分條
    public Text[] _Score = new Text[4];     // 分數面板
    public float Speed;                     // 積分條上升速度
    private float[] score = new float[4];   // 分數
    private float highScore = 0;            // 最高分

    void Start () {
		
	}
    /**
     * 設定分數
     */
    void Awake()
    {
        for (int i = 0; i < 4; i++) {
            string Score = _Score[i].text;
            score[i] = float.Parse(Score);
            if (highScore < score[i])
                highScore = score[i];
        }
        NormalizeScore();
    }
    /**
     * 簡易正規化
     */
    private void NormalizeScore() {
        for (int i = 0; i < 4; i++) {
            score[i] /= highScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_Player[i].fillAmount < score[i])
                _Player[i].fillAmount += Speed;
        }
    }
}
