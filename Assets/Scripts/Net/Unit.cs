using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Unit : NetworkBehaviour
{
    public GameObject[] effectList;

    public float maxHP = 50;
    public Text uiText;
    [SyncVar]
    private float hp;

    void Start()
    {
        hp = maxHP;
    }

    void Update()
    {
        if (hasAuthority)
        {
            if (hp < maxHP)
            {
                hp += Time.deltaTime * 5;
                CmdSetHp(hp);
            }
            else
            {
                hp = maxHP;
            }
            if (Input.GetMouseButton(1))
            {
                hp = 0;
                CmdSetHp(hp);
            }
        }
        uiText.text = "HP:" + ((int)hp).ToString();
    }

    //client 叫 server 做事
    [Command]
    void CmdSetHp(float newHp)
    {
        hp = newHp;
        RpcPlayEffect(0);
    }

    //server 叫 client 做事
    [ClientRpc]
    void RpcPlayEffect(int index)
    {
        Destroy(Instantiate(effectList[index], transform.position, Quaternion.identity), 5);
    }
}