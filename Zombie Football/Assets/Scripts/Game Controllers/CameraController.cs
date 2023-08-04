using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    
    public List<GameObject> players;
    public GameObject ball;

    private Vector3 averagePlayerPosition;

    private void Start()
    {
        foreach(var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(player.transform.GetChild(0).gameObject);
        }
        
    }

    // LateUpdate for camera movement
    void LateUpdate()
    {
        CameraZoom();
        CameraRotate();
    }

    void CameraRotate()
    {
        averagePlayerPosition = ball.transform.position * players.Count; //Weight average in favour of ball
        
        foreach (GameObject player in players)
        {
            averagePlayerPosition += player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.position; //Train-wreck, I know
        }

        if(players.Count != 0) //Protect from division by 0
            averagePlayerPosition /= players.Count * 2; //count doubled because of ball weighting

        Quaternion targetRotation = Quaternion.LookRotation(averagePlayerPosition - transform.position);
        
        transform.rotation =  Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed *  Time.deltaTime); //slerped so no smooth rotate
    }

    void CameraZoom()
    {
        //MAYBE DO THIS?
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(averagePlayerPosition,1);
    }
}
