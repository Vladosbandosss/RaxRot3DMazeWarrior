using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    private PlayerScript playerScript;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerScript = GetComponent<PlayerScript>();
    }

    private void Start()
    {
        GamePlayController.instance.DisplayText(health);
    }
    public void ApplyDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
        }

        GamePlayController.instance.DisplayText(health);
        if (health == 0)
        {
            playerScript.enabled = false;
            anim.Play(MyTags.DEADANIMATION);

            GamePlayController.instance.isPlayerAlive = false;

            GamePlayController.instance.GameOver();
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == MyTags.COINTAG)
        {
            target.gameObject.SetActive(false);

            GamePlayController.instance.CoinCollected();
            SoundManger.instance.PlayCoinSound();
        }   
    }

}
