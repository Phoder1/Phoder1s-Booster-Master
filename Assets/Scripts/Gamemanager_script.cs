using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager_script : MonoBehaviour
{
    AudioSource backgroundmusic;
    static Gamemanager_script instance;
    void Wake()
    {
        if (Gamemanager_script.instance != this)
        {
            Destroy(gameObject);
        }else
        {
            Gamemanager_script.instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        backgroundmusic = GetComponent<AudioSource>();
        backgroundmusic.Play();
        backgroundmusic.loop = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
