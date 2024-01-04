using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField]private float attackrange,attackspeed,bulletspeed;
    [SerializeField]private Transform attackpoint;
    [SerializeField]private GameObject bullet;
    [SerializeField]private PhotonView photonView;
    [SerializeField]private Animator anim;
    private bool canattack=true;
    void Start()
    {
        
    }

    void Update()
    {
        if(photonView.IsMine)
        {
          AttackArea();
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color=Color.green;
        Gizmos.DrawWireSphere(transform.position,attackrange);
    }
    void AttackArea()
    { 
        if (Input.GetKeyDown(KeyCode.Mouse0)&&CheckFireRange()&&canattack)
        {
            canattack=false;
            anim.SetTrigger("attack");
        }
    }
    public void ResetAttack()
    {
        if(!photonView.IsMine)
       return;

       canattack=true;
       Debug.Log("saldırı bitti");
    }
    public void SpawnBullet()
    {
       if(!photonView.IsMine)
       return;
        
       Debug.Log("mermi ateşlendi");
      GameObject blt = PhotonNetwork.Instantiate(bullet.name, attackpoint.position, Quaternion.identity);

      Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector2 direction = (mousePos - (Vector2)blt.transform.position).normalized;

      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

      blt.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

      blt.GetComponent<Rigidbody2D>().velocity = direction * bulletspeed;

    }
    void Destroybullet(GameObject blt)
    {
         PhotonNetwork.Destroy(blt);
    }
    bool CheckFireRange()
    {
        Vector2 mousepos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float between=mousepos.x-attackpoint.position.x;
        between=between/Mathf.Abs(between);
        if (Vector2.Distance(transform.position,mousepos)<=attackrange&&GetComponent<PositionControl>().facingright==between)
        {
            return true;
        }
        return false;
    }
}
