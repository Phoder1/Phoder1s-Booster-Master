using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_script : MonoBehaviour
{
    [SerializeField] bool debugon = false;
    [SerializeField] float rotationfriction = 0.3f;
    [SerializeField] float destroyforce = 4f;
    public float thrustforce;
    [SerializeField] float thrustpressforce;
    [SerializeField] float rotationsens = 2f;
    [SerializeField] AudioSource Boostersound;
    [SerializeField] AudioSource collisionsound;
    [SerializeField] AudioSource scratchsound;
    [SerializeField] AudioSource finishsound;
    private float rotationspeed = 0f;
    private float slowforce = 0f;
    [SerializeField] float maxslowspeed = 20f;
    public static Rocket_script instance;
    Rigidbody rb;
    Collider col;
    private float vely = 0f;
    private float velx = 0f;
    private Vector3 transformspeed;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        destroyforce *= rb.mass;
    }

    // Update is called once per frame
    void Update()
    {

        Handlecontrols();
        if (debugon) { Debugon(); }


    }

    private void Debugon()
    {
        Debug.Log("Rotation Friction :" + rotationfriction);
        Debug.Log("Rotation Speed :" + rotationspeed);
        Debug.Log("Rotation Sens :" + rotationsens);
    }

    private void OnCollisionEnter(Collision collision)
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
    private void Handlecontrols()
    {

        rotationspeed -= Input.GetAxis("Horizontal") * rotationsens * Time.deltaTime;
        transform.Rotate(transform.forward, rotationspeed);
        rotationspeed *= Mathf.Pow((1f - (rotationfriction / 100)), Time.deltaTime);

        if (Input.GetButton("Jump"))
        {
            rb.AddForce(rb.mass * transform.up * thrustforce * Time.deltaTime * (Convert.ToInt32(Input.GetButtonDown("Jump")) * (thrustpressforce - 1) + 1));
            if (!Boostersound.isPlaying)
            {
                Boostersound.Play();
            }
            if (Boostersound.volume != 1)
            {
                Boostersound.volume = 1;
            }
        }
        else
        {
            if (Boostersound.volume <= 0.01)
            {
                Boostersound.Stop();
            }
            else
            {
                Boostersound.volume -= 1 * Time.deltaTime;
            }


        }

    }

    public void Slowmode(float amount)
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
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * Mathf.Pow((1 - (slowforce * (1 - Mathf.Abs(transformspeed.normalized.y)) * maxslowspeed / 100)), Time.deltaTime), 0f ) * Mathf.Pow((1 - (slowforce * (1 - Mathf.Abs(transformspeed.normalized.y)) * maxslowspeed / 100)), Time.deltaTime);
        vely = rb.velocity.y * Mathf.Pow(1f - (maxslowspeed * slowforce * Mathf.Abs(transformspeed.normalized.y)) / 100, Time.deltaTime);
        velx = rb.velocity.x * Mathf.Pow(1f - (maxslowspeed * slowforce * Mathf.Abs(transformspeed.normalized.x)) / 100, Time.deltaTime);
        rb.velocity = new Vector3(velx, vely, 0f);
    }

}

