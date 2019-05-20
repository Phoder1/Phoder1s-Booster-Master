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
        transform.localEulerAngles = Vector3.forward * Mathf.Clamp(transform.localEulerAngles.z, 300f, 359.9f) + Vector3.up* transform.localEulerAngles.y;
        if (Input.GetKey(KeyCode.LeftShift))
        {

            if (transform.localEulerAngles.z > 300f)
            {

                transform.Rotate(-Vector3.forward * 45f * (Time.deltaTime / opentime), Space.Self);
            }
            Rocket_script.instance.Slowmode(transform.localEulerAngles.z);
        }
        else if (transform.localEulerAngles.z <= 358f)
        {
            transform.Rotate(Vector3.forward * 45f * (Time.deltaTime / opentime), Space.Self);
        }
        else
        {
            transform.localEulerAngles = Vector3.forward * 359f + Vector3.up * transform.localEulerAngles.y;
        }

    }
}
