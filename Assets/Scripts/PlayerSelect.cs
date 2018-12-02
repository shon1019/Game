using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class PlayerSelect : MonoBehaviour {

    private player_Info data;
    public player_Info Data
    {
        get
        {
            return data;
        }

        set
        {
            data = value;
        }
    }


    RawImage character;

    private void Start()
    {
        data = new player_Info(0,new Color(0,0,0),0);
        string name = this.transform.GetChild(0).GetComponent<Text>().text;
        char[] ID = name.Substring(name.Length - 1).ToCharArray();
        data.Player_ID = ID[0] - 49;
        character = this.transform.GetChild(1).GetComponentInChildren<RawImage>();

        //Debug.Log(name);
        //Debug.Log(data.Player_ID);
        //Debug.Log(this.transform.GetChild(2).gameObject.name);

    }

}
