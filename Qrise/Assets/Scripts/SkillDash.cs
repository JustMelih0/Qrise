using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SkillDash : MonoBehaviour
{
    private PhotonView photonView;
    [SerializeField]private Animator anim;
     [SerializeField]private Rigidbody2D rgb2d;
    [SerializeField]private float dashspeed;
    private bool candash=true,isdashing;
    void Start()
    {
        photonView=GetComponent<PhotonView>();
    }

  
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            DashArea();
        }
    }
    void DashArea()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)&&GetComponent<CharacterMovement>().CheckGround()&&candash==true)
        {
            candash=false;
            isdashing=true;
            anim.SetTrigger("dashing");
        }
        if (isdashing)
        {
            rgb2d.velocity=new Vector2(dashspeed*GetComponent<PositionControl>().facingright,0);
        }      
    }
    public void ResetDash()
    {
        isdashing=false;
        candash=true;
    }
}
