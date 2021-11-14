using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;

    private Enemy enemy;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
        }


        if (health == 0)
        {
            enemy.enabled = false;
            anim.SetTrigger(MyTags.DEADTRIGGER);

            Invoke(nameof(DeactiveEnemy), 3f);
        }
    }

    void DeactiveEnemy()
    {
        gameObject.SetActive(false);
    }
}
