using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menurocket_script : MonoBehaviour
{
    public float thrustforce;

    [SerializeField] GameObject destroyedship;


    [SerializeField] ParticleSystem jeteffectL;
    [SerializeField] ParticleSystem jeteffectR;

    public static Menurocket_script instance;
    Rigidbody rb;
    Collider col;
    enum State { Alive, Dead, Trancending };
    State state = State.Alive;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (state == State.Alive) Handlecontrols();


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == State.Alive)
        {
            Instantiate(destroyedship, transform.position, transform.rotation);
            Destroy(gameObject);
            state = State.Dead;
        }
    }
    private void Handlecontrols()
    {
        jeteffectL.Play();
        jeteffectR.Play();
        rb.AddForce(rb.mass * transform.up * thrustforce * Time.deltaTime);
    }

}

