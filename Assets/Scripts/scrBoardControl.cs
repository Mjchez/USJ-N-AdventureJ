using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBoardControl : MonoBehaviour
{
    public GameObject door;
    scrPlayerMove playerMove;
    public scrCarControl carControl;
    public AudioSource sfxCarMotor;
    public Transform carSeat;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool TryingToBoard(){
        playerMove.transform.parent = carSeat;
        playerMove.transform.localPosition = Vector3.zero;
        playerMove.transform.localRotation=Quaternion.identity;
        //playerMove.gameObject.SetActive(false);
        door.transform.rotation=Quaternion.Euler(0,0,0);
        carControl.onBoard=true;
        playerMove.wantToBoard=null;
        sfxCarMotor.Play();
        return true;

    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            door.transform.rotation=Quaternion.Euler(0,60,0);
            playerMove = other.gameObject.GetComponent<scrPlayerMove>();
            playerMove.wantToBoard=TryingToBoard;
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            door.transform.rotation=Quaternion.Euler(0,0,0);

            if(playerMove)
            {
                sfxCarMotor.Pause();
                door.transform.rotation=Quaternion.Euler(0,0,0);
                playerMove.wantToBoard=null;
                playerMove=null;
            }
        }
    }
}
