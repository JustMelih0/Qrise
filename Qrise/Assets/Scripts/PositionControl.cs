using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PositionControl : MonoBehaviour
{
    public short facingright;

private float currentpos;
    private PhotonView photonView;

   private void Start() {
    photonView=GetComponent<PhotonView>();
    currentpos=transform.position.x;
   }
    void FixedUpdate()
    {

      if (photonView.IsMine)
      {
         float movex=transform.position.x;
        if (movex>currentpos&&facingright==-1||movex<currentpos&&facingright==1)
        {
            Debug.Log("döndü");
            Flip();
        }
        currentpos=movex;
      }

       
    }
    void Flip()
    {
        facingright*=-1;
      transform.localScale=new Vector2(transform.localScale.x*-1,transform.localScale.y);
    }
}
