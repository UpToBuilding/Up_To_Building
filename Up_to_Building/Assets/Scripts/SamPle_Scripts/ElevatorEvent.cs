using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorEvent : MonoBehaviour
{

    public UnityEvent ElevatorTrigger;

    public AudioSource audio;
    public Animator ani;
    private bool count = false;
    void Start()
    {
        ani = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        Player player = Player.PlayerTransform.gameObject.GetComponent<Player>();
        player.initElv += countTrigger;
    }

    public void countTrigger()
    {
       count = false;
    }

  


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!count)
            {
                count = true;
                ani.SetTrigger("start");
                audio.Play();
            }
        }
    }

}
