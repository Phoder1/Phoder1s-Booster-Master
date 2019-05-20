using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyedship_script : MonoBehaviour
{
    [SerializeField] AudioSource explosionsound;
    [SerializeField] ParticleSystem deatheffect;
    // Start is called before the first frame update
    void Start()
    {
        explosionsound.Play();
        deatheffect.Play();
        Invoke("Resetlevel", 3f);

    }

    private void Resetlevel()
    {
        Gamemanager_script.instance.Resetlevel();
    }
}
