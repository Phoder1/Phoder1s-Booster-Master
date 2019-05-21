using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager_script : MonoBehaviour
{
    private int currentlevel = 0;
    AudioSource backgroundmusic;
    [SerializeField] GameObject menurocket;
    public static Gamemanager_script instance;
    Quaternion menurocketquaternion;
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
        menurocketquaternion = Quaternion.Euler(0f, 0f, 30.643f);
        backgroundmusic = GetComponent<AudioSource>();
        backgroundmusic.Play();
        backgroundmusic.loop = true;
        currentlevel = SceneManager.GetActiveScene().buildIndex;


    }

    // Update is called once per frame
    void Update()
    {
        print("current: " + currentlevel);
        print("count: " + SceneManager.sceneCountInBuildSettings);
        
    }

    public void Passlevel()
    {
        currentlevel++;
        if(SceneManager.sceneCountInBuildSettings ==  currentlevel) { currentlevel = 0; }
        SceneManager.LoadScene(currentlevel);

    }

    public void Resetlevel()
    {
        SceneManager.LoadScene(currentlevel);
    }

    public void Restartmenu()
    {
        Instantiate(menurocket, new Vector3(-4.69f, 20.44f, 0f), menurocketquaternion);
    }
}
