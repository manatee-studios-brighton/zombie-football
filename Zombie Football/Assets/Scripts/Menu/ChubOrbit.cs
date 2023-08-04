using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChubOrbit : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    [SerializeField] private bool up;
    [SerializeField] private bool forward;
    [SerializeField] private bool right;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 axis = (up ? Vector3.up : Vector3.zero) + (forward ? Vector3.forward : Vector3.zero) + (right ? Vector3.right : Vector3.zero);
        transform.RotateAround(Vector3.zero, axis, speed * Time.deltaTime);
    }
}
