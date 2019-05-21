using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menurocket_script : MonoBehaviour
{
    public float thrustforce;

    [SerializeField] GameObject destroyedship;
    [SerializeField] GameObject jetparticles;


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
        jeteffectL = Instantiate(jeteffectL, transform.position + transform.right * -1f + transform.up * -0.5f, transform.rotation);
        jeteffectL.transform.SetParent(gameObject.transform);
        jeteffectL.Play();
        jeteffectR = Instantiate(jeteffectR, transform.position + Vector3.right * 1f + Vector3.up * -0.5f, transform.rotation);
        jeteffectR.transform.SetParent(gameObject.transform);
        jeteffectR.Play();
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
        rb.AddForce(rb.mass * transform.up * thrustforce * Time.deltaTime);
    }

}

