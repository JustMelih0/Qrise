using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviour
{
   [SerializeField] private float health;
    bool die; 
    private Animator anim;
    private PhotonView photonView;
    void Start()
    {
        anim=GetComponent<Animator>();
        photonView=GetComponent<PhotonView>();
    }
    [PunRPC]
    public void Hurt(float damage)
    {     
      if (die==false&&photonView.IsMine)
      {
         health-=damage;
        photonView.RPC("UpdateHealth", RpcTarget.AllBuffered, health);
        if (health<=0)
       {
            die=true;
            anim.SetTrigger("die");
           Invoke(nameof(DestroyMe),0.6f);
         }    
      }
    }
    void DestroyMe()
    {
       transform.position=Vector2.zero;
        health=100;
        photonView.RPC("UpdateHealth", RpcTarget.AllBuffered, health);
        die=false;
    }

     [PunRPC]
    private void UpdateHealth(float newHealth)
    {
        health = newHealth;
    }
}
