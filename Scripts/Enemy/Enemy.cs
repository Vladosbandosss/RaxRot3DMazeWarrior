using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;

    private Rigidbody rb;
    private Animator anim;

    private float enemySpeed = 10f;

    private float enemyWatchTreshHold = 70f;
    private float enemyAttackTreshHold = 6f;

    public GameObject damagePoint;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYERTAG);

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (GamePlayController.instance.isPlayerAlive)
        {
            EnemyAI();
        }
        else
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUNANIMATION)||
               anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.ATTACKANIMATION))
            {
                anim.SetTrigger(MyTags.STOPTRIGGER);
            }
        }
      
    }

    private void EnemyAI()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;
        direction.Normalize();

        Vector3 velocity = direction * enemySpeed;

        if (distance > enemyAttackTreshHold && distance < enemyWatchTreshHold)
        {
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.ATTACKANIMATION))
            {
                anim.SetTrigger(MyTags.STOPTRIGGER);
            }

            anim.SetTrigger(MyTags.RUNTRIGGER);

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        }else if (distance < enemyAttackTreshHold)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUNANIMATION))
            {
                anim.SetTrigger(MyTags.STOPTRIGGER);
            }

            anim.SetTrigger(MyTags.ATTACKTRIGGER);

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }
        else
        {
            rb.velocity = new Vector3(0f, 0f, 0f);

            if(anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUNANIMATION)||
                anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUNANIMATION))
            {
                anim.SetTrigger(MyTags.STOPTRIGGER);
            }
        }
    }

    void ActivateDamagePoint()
    {
        damagePoint.SetActive(true);
    }

    void DeactivateDamagePoint()
    {
        damagePoint.SetActive(false);
    }
}//class
