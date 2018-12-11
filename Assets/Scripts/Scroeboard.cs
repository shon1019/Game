using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scroeboard : MonoBehaviour {

    public Image[] _Player = new Image[4];  // 積分條
    public Text[] _Score = new Text[4];     // 分數面板
    public GameObject[] _Crown = new GameObject[4];   // 皇冠
    public float Speed;                     // 積分條上升速度
    private float[] score = new float[4];   // 分數
    private float highScore = 1;            // 最高分，設1為防呆
    private int lessPlayer = 4;             // 尚未跑完積分人數
    private bool[] lessPlayerCheck = new bool[4];   // 尚未跑完積分之人
    private bool work;                      // 是否開始動作
    /**
     * 測試用
     * 設定分數
     */
    /*void Awake()
    {
        lessPlayer = 4;
        for (int i = 0; i < 4; i++) {
            lessPlayerCheck[i] = true;
            string Score = _Score[i].text;
            score[i] = float.Parse(Score);
            if (highScore < score[i])
                highScore = score[i];
        }
        work = true;
    }*/
    /*
     * 提供呼叫並執行
     */
    public void ShowRecord(int[] input) {
        lessPlayer = 4;
        for (int i = 0; i < 4; i++)
        {
            lessPlayerCheck[i] = true;
            score[i] = input[i];
            if (highScore < score[i])
                highScore = score[i];       // 最高分紀錄
        }
        work = true;
    }

    /*
     * 尋找所有人避免漏掉共同第一
     * 播放第一皇冠動畫
     */
    private void ShowCrown() {
        for (int i = 0; i < 4; i++)
        {
            if (score[i] == highScore)
            { 
                _Crown[i].SetActive(true);
                _Crown[i].GetComponent<Animator>().Play("Crown");
            }
        }
        work = false;
    }

    void Update()
    {
        if (work)
        {
            for (int i = 0; i < 4; i++)
            {
                if (_Player[i].fillAmount <= score[i] / highScore && lessPlayerCheck[i])
                {
                    _Player[i].fillAmount += Speed; // 積分條累加
                    int tmpScore = (int)(highScore * _Player[i].fillAmount);    // 暫時當下的面板積分數字
                    if (tmpScore < score[i])
                        _Score[i].text = tmpScore.ToString();                   // 暫時的
                    else
                    {
                        _Score[i].text = score[i].ToString();                   // 最終結果
                        lessPlayer--;                                           // 結束一人積分條
                        lessPlayerCheck[i] = false;                             // 結束此人積分
                        if (lessPlayer == 0)
                        {                                                       // 通通結束
                            lessPlayer--;                                       // 防呆
                            ShowCrown();                                        // 播放第一動畫
                            
                        }
                    }
                }
            }
        }
    }


    /*
     * 按鈕AGAIN
     */
    public void again()
    {
        // 數字等BUILD設定
        SceneManager.LoadScene(1);
        // 重新載入遊戲場景
    }

    /*
     * 按鈕EXIT
     */
    public void exit()
    {
        Application.Quit();
    }
}
