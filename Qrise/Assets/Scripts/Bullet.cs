using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PhotonView photonView;
    [SerializeField]private float damage;
    private void Start() {
        photonView=GetComponent<PhotonView>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        
       if (photonView.IsMine&&photonView!=null)
       {
        if (other.gameObject.tag=="Player"&&!other.gameObject.GetComponent<PhotonView>().IsMine)
        {
          other.gameObject.GetComponent<PhotonView>().RPC("Hurt", RpcTarget.AllBuffered, damage);
        }
         PhotonNetwork.Destroy(gameObject);
       } 
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (photonView.IsMine&&photonView!=null)
       {
        if (other.gameObject.tag=="Player"&&!other.gameObject.GetComponent<PhotonView>().IsMine)
        {
          other.gameObject.GetComponent<PhotonView>().RPC("Hurt", RpcTarget.AllBuffered, damage);
        }
         PhotonNetwork.Destroy(gameObject);
       }
    }
}
