using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings_script : MonoBehaviour
{
    [SerializeField] float opentime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Rocket_script.instance.Slowmode(transform.localEulerAngles.z);
            if (transform.localEulerAngles.z > 300f || transform.localEulerAngles.z == 0f)
            {

                transform.Rotate(-Vector3.forward * 45f * (Time.deltaTime / opentime), Space.Self);
            }else
            {
                transform.localEulerAngles = Vector3.forward * 300f + Vector3.up * transform.localEulerAngles.y;
            }
        }
        else if (transform.localEulerAngles.z < 360f && transform.localEulerAngles.z > 250f)
        {
            transform.Rotate(Vector3.forward * 45f * (Time.deltaTime / opentime), Space.Self);
        }
        else
        {
            transform.localEulerAngles = Vector3.forward * 0f + Vector3.up * transform.localEulerAngles.y;
        }

    }
}
