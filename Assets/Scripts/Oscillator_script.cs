using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator_script : MonoBehaviour
{
    [SerializeField] bool global = false;
    [SerializeField] Vector3 targetpoint = new Vector3(0f,0f,0f);
    private Vector3 origin;
    [SerializeField] float cycletime = 1f;
    [SerializeField] float starttime = 0f;
    [SerializeField] AnimationCurve sincurve;
    [Range(0f, 1f)]
    private float normalizedsin;
    // Start is called before the first frame update
    void Start()
    {
        sincurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        origin = transform.position;
        if (!global) targetpoint = origin + targetpoint;
    }

    // Update is called once per frame
    void Update()
    {
        normalizedsin = Mathf.Max(Mathf.Sin(((Time.time + starttime) / cycletime) * 2 * Mathf.PI - 0.5f * Mathf.PI)/2 + 0.5f, Mathf.Epsilon);
        sincurve.AddKey(Time.time, normalizedsin);
        transform.position = Vector3.Lerp(origin, targetpoint, normalizedsin);
        
    }
}
