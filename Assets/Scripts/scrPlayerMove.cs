using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerMove : MonoBehaviour
{
    public Rigidbody rdb;
    public Animator anim;
    Vector3 mov;
    GameObject gamecamera;
    public GameObject cameraAim;
    public GameObject spine;
    public delegate bool WantToBoard();
    public WantToBoard wantToBoard;
    public GameObject saiDoCarroObj;
    public scrCarControl carControlScr;

    public enum State{
        Combat,
        Board,
        Died
    }

    public State state;

    void Start()
    {
        gamecamera = Camera.main.gameObject;
        ChangeState();
    }

    IEnumerator Combat(){
        while(state==State.Combat)
        {
            Vector3 cameraRelativeMov = gamecamera.transform.TransformDirection(mov);
            rdb.velocity= new Vector3(cameraRelativeMov.x*4, rdb.velocity.y, cameraRelativeMov.z*4);
            float radtogo = Vector3.Dot(transform.forward, -gamecamera.transform.right)*5;
            
            transform.Rotate(0,radtogo,0);
            Vector3 locVel= transform.InverseTransformDirection(rdb.velocity).normalized;

            anim.SetFloat("Walk", locVel.z);
            anim.SetFloat("SideWalk", locVel.x + radtogo);
            anim.SetFloat("Speed",rdb.velocity.magnitude + radtogo);

            yield return new WaitForFixedUpdate();
        }
        ChangeState();
    }

    IEnumerator Board(){
        rdb.isKinematic=true;
        Collider[] cols = GetComponentsInChildren<Collider>();
        foreach(Collider col in cols)
        {
            col.enabled =false;
        }

        while(state==State.Board)
        {
           yield return new WaitForFixedUpdate(); 
        }
        ChangeState();
    }

    IEnumerator Died(){
        while(state==State.Died)
        {
           yield return new WaitForFixedUpdate(); 
        }
        ChangeState();
    }

    void ChangeState(){
        StopAllCoroutines();
        StartCoroutine(state.ToString());
    }

    void ChangeState(State mystate){
        state = mystate;
        StopAllCoroutines();
        StartCoroutine(mystate.ToString());
    }

    void Update()
    {
        mov = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        cameraAim.transform.forward = gamecamera.transform.forward;

        if(Input.GetKeyDown(KeyCode.E)){
            if(wantToBoard()){
                ChangeState(State.Board);
            }
        }

        if(Input.GetKeyDown(KeyCode.F)){
            if(state == State.Board){
                transform.localPosition = saiDoCarroObj.transform.localPosition;
                carControlScr.onBoard = false;
                rdb.isKinematic = false;
                transform.localRotation = Quaternion.identity;
                ChangeState(State.Combat);
                
                Collider[] cols = GetComponentsInChildren<Collider>();
                foreach(Collider col in cols)
                {
                    col.enabled = true;
                }
            }
        }

        
        
        if(Input.GetButtonDown("Fire1")){
            anim.SetBool("Shoot",true);
        }else{
            anim.SetBool("Shoot",false);
        }
    } 

    private void OnAnimatorIK(int layerIndex){
        anim.SetBoneLocalRotation(HumanBodyBones.Spine, cameraAim.transform.localRotation);
    }
}
