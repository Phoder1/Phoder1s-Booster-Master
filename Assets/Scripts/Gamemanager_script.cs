using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager_script : MonoBehaviour
{
    private int currentlevel = 0;
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
        currentlevel++;
        SceneManager.LoadScene(currentlevel);

    }

    public void Resetlevel()
    {
        SceneManager.LoadScene(currentlevel);
    }
}
