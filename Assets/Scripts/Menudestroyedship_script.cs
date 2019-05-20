using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menudestroyedship_script : MonoBehaviour
{
    [SerializeField] ParticleSystem deatheffect;
    // Start is called before the first frame update
    void Start()
    {
        deatheffect.Play();
        Invoke("Resetlevel", 10f);

    }

    private void Resetlevel()
    {
        Gamemanager_script.instance.Restartmenu();
        Destroy(gameObject);
    }
}
