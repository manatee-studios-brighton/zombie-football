using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipNormals : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
            normals[i] = -normals[i];

        mesh.normals = normals;

        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            int[] tris = mesh.GetTriangles(i);
            for (int j = 0; j < tris.Length; j += 3)
            {
                //swap order of tri vertices
                (tris[j], tris[j + 1]) = (tris[j + 1], tris[j]);
            }
            
            mesh.SetTriangles(tris, i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
