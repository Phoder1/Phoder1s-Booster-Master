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
        deatheffect = Instantiate(deatheffect, transform.position + transform.right * -1f + transform.up * -0.5f, transform.rotation);
        deatheffect.Play();
        explosionsound.Play();
        Invoke("Resetlevel", 3f);

    }

    private void Resetlevel()
    {
        Gamemanager_script.instance.Resetlevel();
    }
}
