using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorEvent : MonoBehaviour
{

    public UnityEvent ElevatorTrigger;

    public AudioSource audio;
    public Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ani.SetTrigger("start");
            audio.Play();
            
        }
    }

}
