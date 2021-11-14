using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmmount = 2;

    public LayerMask playerLAyer;
    

   
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.1f, playerLAyer);

        if (hits.Length > 0)
        {
            if (hits[0].gameObject.tag == MyTags.PLAYERTAG)
            {
                hits[0].gameObject.GetComponent<PlayerHealth>().ApplyDamage(damageAmmount);
            }
        }
    }
}
