using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class scrWeaponControl : MonoBehaviour
{
    public Animator anim;
    public VisualEffect vfx;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            anim.SetBool("Shoot",true);
            vfx.Play();
        }else{
            anim.SetBool("Shoot",false);
        }    
    }
}
