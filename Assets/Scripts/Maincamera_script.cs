using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maincamera_script : MonoBehaviour
{
    [SerializeField] GameObject mainplayer;
    [SerializeField] float zdistance = 80f;
    // Start is called before the first frame update
    void Start()
    {
        mainplayer = GameObject.Find("Rocket");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainplayer != null)
        {
            transform.position = mainplayer.transform.position - zdistance * Vector3.forward;
        }

    }
}
