using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    public bool goalScored;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag($"Ball"))
        {
            goalScored = true;
            other.gameObject.transform.position = new Vector3(0, 5, 0);
        }
    }
}
