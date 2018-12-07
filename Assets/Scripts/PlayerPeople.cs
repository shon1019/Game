﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct player_Info
{
    private int _player_ID;
    public int Player_ID
    {
        get
        {
            return _player_ID;
        }

        set
        {
            _player_ID = value;
        }
    }

    public player_Info(int id, Color c,int num)
    {
        _player_ID = id;
    }
}
public class Info{
    public Info(int p, player_Info[] data) {
        People = p;
        Player_Infos = new player_Info[p];
        Player_Infos = data;
    }

    private int _people;
    public int People
    {
        get
        {
            return _people;
        }

        set
        {
            _people = value;
        }
    }

    private player_Info[] _player_Infos;
    public player_Info[] Player_Infos
    {
        get
        {
            return _player_Infos;
        }

        set
        {
            _player_Infos = value;
        }
    }
}

public class PlayerPeople : MonoBehaviour {
    public GameObject people;
    public GameObject select;
    private int player;
    void CallSelect() {
        select.SetActive(true);
        people.SetActive(false);
    }

    void CallPeople() {
        select.SetActive(false);
        people.SetActive(true);
    }

    public void Btn_Back() {

        CallPeople();
    }
    public void Btn_Go() {
        player_Info[] data = new player_Info[player];
        for (int i=0; i < player; i++)
            data[i] = select.transform.GetChild(i).GetComponent<PlayerSelect>().Data;
        Info info = new Info(player, data);
        SceneManager.LoadScene(1);
    }

    public void Btn2_Click() {
        player = 2;
        CallSelect();
 
    }

}
