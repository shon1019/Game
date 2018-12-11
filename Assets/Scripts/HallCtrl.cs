using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 簡易大廳轉換按鈕
 */
public class HallCtrl : MonoBehaviour {

    public void Btn_Start() {
        SceneManager.LoadScene(1);
    }
}
