using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maincamera_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameObject.Find("Rocket").transform.position - 50 * Vector3.forward;
    }
}
