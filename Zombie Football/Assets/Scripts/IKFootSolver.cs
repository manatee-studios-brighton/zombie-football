using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    public Transform hips;
    public Transform foot;
    public float stepSize;
    
    private Vector3 _potentialStepPosition = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        int layerMask = 1 << 2;
        layerMask = ~layerMask;

        Vector3 rayOrigin = hips.position + hips.TransformDirection(Vector3.forward)*(stepSize*0.5f);
        Vector3 rayDirection = hips.TransformDirection(Vector3.down);
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, 10, layerMask))
        {
            _potentialStepPosition = hit.point;

            if (Vector3.Distance(foot.position, _potentialStepPosition) > stepSize)
            {
                transform.position = _potentialStepPosition;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_potentialStepPosition, 0.1f);
    }
}
