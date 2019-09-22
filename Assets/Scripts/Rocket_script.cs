using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_script : MonoBehaviour
{
    [SerializeField] float rotationfriction = 0.3f;
    [SerializeField] float destroyforce = 4f;
    public float thrustforce;
    [SerializeField] float thrustpressforce;
    [SerializeField] float rotationsens = 2f;

    [SerializeField] GameObject destroyedship;

    [SerializeField] AudioSource Boostersound;
    [SerializeField] AudioSource scratchsound;
    [SerializeField] AudioSource finishsound;

    [SerializeField] ParticleSystem jeteffectL;
    [SerializeField] ParticleSystem jeteffectR;
    [SerializeField] ParticleSystem finisheffect;

    private float rotationspeed = 0f;
    private float slowforce = 0f;
    [SerializeField] float maxslowspeed = 20f;
    public static Rocket_script instance;
    Rigidbody rb;
    Collider col;
    private float vely = 0f;
    private float velx = 0f;
    private Vector3 transformspeed;
    enum State { Alive, Dead, Trancending };
    State state = State.Alive;



    // Start is called before the first frame update
    void Start()
    {
        state = State.Alive;
        jeteffectL = Instantiate(jeteffectL, transform.position + transform.right * -1f + transform.up * -0.5f, transform.rotation);
        jeteffectL.transform.SetParent(gameObject.transform);
        jeteffectR = Instantiate(jeteffectR, transform.position + Vector3.right * 1f + Vector3.up * -0.5f, transform.rotation);
        jeteffectR.transform.SetParent(gameObject.transform);
        instance = this;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        destroyforce *= rb.mass;
    }

    // Update is called once per frame
    void Update()
    {

        if (state == State.Alive) Handlecontrols();
        if (Debug.isDebugBuild && Input.GetKeyDown("l") && state == State.Alive)
        {

            Invoke("Passlevel", 2f);
            finisheffect = Instantiate(finisheffect, transform.position, transform.rotation);
            finishsound.Play();
            finisheffect.Play();
            state = State.Trancending;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= destroyforce)
        {
            if (state == State.Alive)
            {
                Instantiate(destroyedship, transform.position, transform.rotation);
                Destroy(gameObject);
                state = State.Dead;
            }

        }
        else
        {
            scratchsound.volume = collision.relativeVelocity.magnitude / destroyforce;
            scratchsound.Play();
            switch (collision.gameObject.tag)
            {
                case "Goal":

                    if (!finishsound.isPlaying && state == State.Alive)
                    {
                        Invoke("Passlevel", 2f);
                        finishsound.Play();
                        finisheffect = Instantiate(finisheffect, transform.position, transform.rotation);
                        finisheffect.Play();
                        state = State.Trancending;
                    }
                    break;


            }
        }
    }


    private void Passlevel()
    {
        if (state == State.Trancending)
        {
            Gamemanager_script.instance.Passlevel();
        }

    }
    private void Handlecontrols()
    {

        rotationspeed -= Input.GetAxis("Horizontal") * rotationsens * Time.deltaTime;
        transform.Rotate(transform.forward, rotationspeed*Time.deltaTime);
        rotationspeed *= Mathf.Pow((1f - (rotationfriction / 100)), Time.deltaTime);

        if (Input.GetButton("Jump"))
        {
            jeteffectR.Play();
            jeteffectL.Play();
            rb.AddForce(rb.mass * transform.up * thrustforce * Time.deltaTime * (Convert.ToInt32(Input.GetButtonDown("Jump")) * (thrustpressforce - 1) + 1));
            if (!Boostersound.isPlaying)
            {
                Boostersound.Play();
            }
            if (Boostersound.volume != 0.7f)
            {
                Boostersound.volume = 0.7f;
            }
        }
        else
        {
            jeteffectR.Stop();
            jeteffectL.Stop();
            if (Boostersound.volume <= 0.01)
            {
                Boostersound.Stop();
            }
            else
            {
                Boostersound.volume -= 0.2f * Time.deltaTime;
            }


        }

    }

    public void Slowmode(float amount)
    {
        if (state == State.Alive)
        {
            if (amount == 0f)
            {
                slowforce = 0f;
            }
            else
            {
                slowforce = (1 - Mathf.Max(((amount - 300f) / 60f), 0f));
            }
            transformspeed = transform.TransformVector(Vector3.up * Vector3.Dot(rb.velocity, transform.up.normalized));
            vely = rb.velocity.y * Mathf.Pow(1f - (maxslowspeed * slowforce * Mathf.Abs(transformspeed.normalized.y)) / 100, Time.deltaTime);
            velx = rb.velocity.x * Mathf.Pow(1f - (maxslowspeed * slowforce * Mathf.Abs(transformspeed.normalized.x)) / 100, Time.deltaTime);
            rb.velocity = new Vector3(velx, vely, 0f);
        }
    }


}

