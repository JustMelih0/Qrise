using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    [SerializeField]private float speed;
    public static CameraMovement Instance;
    private void Awake() {
        if(Instance==null)
        Instance=this;
    }
    

    void Update()
    {
        if(target!=null)
        transform.position=Vector3.MoveTowards(transform.position,
        new Vector3(target.position.x,target.position.y,-10f)
        ,speed*Time.deltaTime);
    }
    
}
