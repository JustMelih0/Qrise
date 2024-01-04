using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]private PhotonView photonView;
    [SerializeField]private Animator anim;
    [SerializeField]private float speed,jumpforce,groundrad;
    [SerializeField]private Rigidbody2D rgb2d;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private Transform groundpoint;
    
    void Start()
    {
        if(photonView.IsMine)
       CameraMovement.Instance.target=transform;  
    } 
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Movement();
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color=Color.blue;
        Gizmos.DrawWireSphere(groundpoint.position,groundrad);
    }
    void Movement()
    {
        anim.SetFloat("speed",Mathf.Abs(rgb2d.velocity.x));
        anim.SetFloat("jump",rgb2d.velocity.y);
        anim.SetBool("isGrounded",CheckGround());

        if (Input.GetKey(KeyCode.D))
        {
            rgb2d.velocity=new Vector2(speed,rgb2d.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rgb2d.velocity=new Vector2(-speed,rgb2d.velocity.y);
        }
        else
        {
            rgb2d.velocity=new Vector2(0,rgb2d.velocity.y);
        }
        if (Input.GetKey(KeyCode.W)&&CheckGround()==true)
        {
            rgb2d.velocity=new Vector2(rgb2d.velocity.x,jumpforce);
        }
    }
    public bool CheckGround()
    {
        Collider2D[] grounds=Physics2D.OverlapCircleAll(groundpoint.position,groundrad,groundLayer);
        foreach (Collider2D point in grounds)
        {
            return true;
        }
        return false;
    }
}
