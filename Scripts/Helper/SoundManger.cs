using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static SoundManger instance;

    public AudioSource coinSound;

    private void Awake()
    {
        MakeInstance();
    }
    private void MakeInstance()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

    }

    public void PlayCoinSound()
    {
        coinSound.Play();
    }
}
