using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_script : MonoBehaviour
{
    [SerializeField] bool debug;
    [SerializeField] float rotationfriction = 0.3f;
    [SerializeField] float destroyforce = 4f;
    public float thrustforce;
    [SerializeField] float thrustpressforce;
    [SerializeField] float rotationsens = 2f;
    [SerializeField] AudioSource boostersound;
    [SerializeField] AudioSource collisionsound;
    [SerializeField] AudioSource scratchsound;
    [SerializeField] AudioSource finishsound;
    private float rotationspeed = 0f;
    Rigidbody rb;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        destroyforce *= rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        
        Handlecontrols();
        if (debug) { Debugon(); }
        
        
    }

    private void Debugon()
    {
        Debug.Log("Rotation Friction :" + rotationfriction);
        Debug.Log("Rotation Speed :" + rotationspeed);
        Debug.Log("Rotation Sens :" + rotationsens);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= destroyforce)
        {
            collisionsound.Play();
        }
        else
        {
            scratchsound.volume = collision.relativeVelocity.magnitude / destroyforce;
            scratchsound.Play();

            switch (collision.gameObject.tag)
            {
                case "Goal":

                    Gamemanager_script.instance.Passlevel();
                    break;


            }
        }
    }
    void Handlecontrols()

    
    {

        rotationspeed -= Input.GetAxis("Horizontal") * rotationsens * Time.deltaTime;
        transform.Rotate(transform.forward, rotationspeed);
        rotationspeed *= Mathf.Pow((1f-(rotationfriction/100)) , Time.deltaTime);

        if (Input.GetButton("Jump"))
        {
            rb.AddForce(rb.mass * transform.up * thrustforce * Time.deltaTime * (Convert.ToInt32(Input.GetButtonDown("Jump")) * (thrustpressforce-1) + 1));
            if (!boostersound.isPlaying)
            {
                boostersound.Play();
            }
            if(boostersound.volume != 1)
            {
                boostersound.volume = 1;
            }
        }
        else
        {
            if (boostersound.volume <= 0.01)
            {
                boostersound.Stop();
            }else
            {
                boostersound.volume -= 1 * Time.deltaTime;
            }


        }

    }
    
    

}

