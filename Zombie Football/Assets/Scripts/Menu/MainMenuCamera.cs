using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public Transform target;

    [SerializeField] private float speed = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position;
        Vector3 axis = (Vector3.up + Vector3.forward + Vector3.right);
        transform.RotateAround(targetPosition, axis, speed * Time.deltaTime);
    }
}
