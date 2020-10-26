using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrExplode : MonoBehaviour
{
    public GameObject explosionVFX;
    public int lives = 0;


    public void Damage(){
        lives--;
        if(lives<1){
           StartCoroutine(Explosion());
        }
    }

    public void DamageHard(){
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 10, Vector3.up, 20);
        yield return new WaitForSeconds(0.1f);

        foreach(RaycastHit hit in hits){
            if(hit.collider.gameObject == gameObject)
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
            scrExplode exp = hit.collider.GetComponent<scrExplode>();     

            Rigidbody rdb =hit.collider.GetComponent<Rigidbody>();
            if(rdb){
                rdb.AddExplosionForce(200, transform.position, 20);
            }
            if(exp)
            {
                exp.DamageHard();
            }
        }
        Destroy(gameObject,0.2f);

    }

}

