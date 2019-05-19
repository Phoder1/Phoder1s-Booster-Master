using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager_script : MonoBehaviour
{
    AudioSource backgroundmusic;
    public static Gamemanager_script instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
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

    public void Passlevel()
    {
        print("NICE!");
    }
}
