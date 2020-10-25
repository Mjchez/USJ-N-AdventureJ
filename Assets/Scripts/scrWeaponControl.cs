using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class scrWeaponControl : MonoBehaviour
{
    public Animator anim;
    public VisualEffect vfxmuzzle;
    public VisualEffect vfxricochete;
    public AudioSource sourceFire01;
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))//if(!anim.GetBool("Shoot") && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ShootSingle());
        }    
    }

    IEnumerator ShootSingle()
    {
        anim.SetBool("Shoot",true);

        yield return new WaitForSeconds(0.1f);
        vfxmuzzle.Play();
        sourceFire01.pitch=Random.Range(0.8f,1.2f);
        sourceFire01.Play();
        Rigidbody rdb=null;
        
        if(Physics.Raycast(vfxmuzzle.transform.position, vfxmuzzle.transform.forward, out RaycastHit hit,100))
        {
            vfxricochete.transform.position=hit.point;
            rdb=hit.collider.GetComponent<Rigidbody>();
        }

        yield return new WaitForSeconds(0.1f);
        vfxricochete.Play();
        if(rdb)
        {
            rdb.AddForce(vfxmuzzle.transform.forward*10, ForceMode.Impulse);
        }


        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Shoot",false);
    }
}
