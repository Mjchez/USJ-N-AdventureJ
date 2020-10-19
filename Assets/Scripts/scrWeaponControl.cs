using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrWeaponControl : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            anim.SetBool("Shoot",true);
        }else{
            anim.SetBool("Shoot",false);
        }    
    }
}
