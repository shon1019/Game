using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour {

    public GameManager gm;
    public ParticleSystem DeadParticle;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ParticleSystem tmp = GameObject.Instantiate(DeadParticle, other.gameObject.transform.position,Quaternion.identity);//particle
            tmp.Play();

            PlayerController tmpPlayerController = other.GetComponent<PlayerController>();//玩冠轉移
            if(tmpPlayerController.LastCollisionPlayer!=-1)
            {
                
                int totalNumber = tmpPlayerController.Points.Count;
                int loseNumber = totalNumber / 4;
                Debug.Log("Tran Crown" + totalNumber + "+" + loseNumber);
                for (int i= totalNumber - 1;i>= totalNumber - loseNumber;i--)
                {
                    StartCoroutine(tmpPlayerController.Points[i].ChangeTarget(GameManager.One.Player[tmpPlayerController.LastCollisionPlayer]));
                }
            }

            other.gameObject.SetActive(false);
            gm.Revived(other.GetComponent<PlayerController>().PlayerId);//復活
            Destroy(tmp.gameObject,1);
        }
    }
}
