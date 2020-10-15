using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerMove : MonoBehaviour
{
    public Rigidbody rdb;
    public Animator anim;
    Vector3 mov;

    GameObject gamecamera;
    void Start()
    {
        gamecamera = Camera.main.gameObject;
    }

    void Update()
    {
        mov = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
    }

    private void FixedUpdate(){
        
        Vector3 cameraRelativeMov = gamecamera.transform.TransformDirection(mov);
        rdb.velocity= new Vector3(cameraRelativeMov.x*4, rdb.velocity.y, cameraRelativeMov.z*4);
        float radtogo = Vector3.Dot(transform.forward, -gamecamera.transform.right)*5;
        
        transform.Rotate(0,radtogo,0);
        Vector3 locVel= transform.InverseTransformDirection(rdb.velocity).normalized;

        anim.SetFloat("Walk", locVel.z);
        anim.SetFloat("SideWalk", locVel.x + radtogo);
        anim.SetFloat("Speed",rdb.velocity.magnitude + radtogo);
    }

}
