using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointEffect : MonoBehaviour
{
    [SerializeField]
    private Animator ani;
    public bool one;
    private AudioSource checkPointSound;

    private void Awake()
    {
        one = true;
        ani = GetComponent<Animator>();
        checkPointSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player")&&one)
        {
            one=false;
            ani.SetTrigger("effect");
        }
    }

    public void PlayCheckPointSound()
    {
        checkPointSound.Play();
    }
}
