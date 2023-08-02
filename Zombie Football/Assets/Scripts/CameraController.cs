using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] players;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 averagePlayerPosition = Vector3.zero;

        foreach (GameObject player in players)
        {
            averagePlayerPosition += player.transform.position;
        }

        if(players.Length != 0)
            averagePlayerPosition /= players.Length;
        
        transform.rotation = Quaternion.LookRotation(averagePlayerPosition - transform.position);
    }
}
