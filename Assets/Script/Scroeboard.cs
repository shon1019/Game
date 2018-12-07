using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroeboard : MonoBehaviour {

    public GameObject _Player1;
    public Text _P1Score;
    public float Speed;

    float score;
    // Use this for initialization
    void Start () {
		
	}

    void Awake()
    {
        string Score = _P1Score.GetComponent<Text>().text;
        score = float.Parse(Score) / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (_Player1.GetComponent<Image>().fillAmount < score)
            _Player1.GetComponent<Image>().fillAmount += Speed;
    }
}
