using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    [Range(1.5f, 5f)]
    public float radius = 2f;
    [Range(0.5f, 5f)]
    public float deformation = 2f;
    
    private Mesh mesh;
    private Vector3[] verticies;
    private Vector3[] modVertices;
    public float espera = 0f;
    public int indexV;
    public float perlNoisVal;
    public float power;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentInChildren<MeshFilter>().mesh;
        verticies = mesh.vertices;
        modVertices = mesh.vertices;
    }


    void RecalculateMesh()
    {
        mesh.vertices = modVertices;
        GetComponentInChildren<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            for (int v = 0; v < modVertices.Length; v++)
            {
                Vector3 distance = modVertices[v] - hit.point;

                float smoothing = 2;
                float force = deformation / (1.5f + hit.point.sqrMagnitude);

                if (distance.sqrMagnitude < radius)
                {
                    if (Input.GetMouseButton(0))
                    {
                        modVertices[v] = modVertices[v] + (Vector3.up * force) / smoothing;
                    }
                    else if (Input.GetMouseButton(1))
                    {
                        modVertices[v] = modVertices[v] + (Vector3.down * force) / smoothing;
                    }
                }
            }
        }
        RecalculateMesh();

        /*for(int i = 0; i < modVertices.Length; i++)
        {
            //modVertices[i] = new Vector3[modVertices[i].x, Mathf.PerlinNoise(modVertices[i].x * perlNoisVal, modVertices[i].z * perlNoisVal), * power, modVertices[i].z;
        }
        mesh.vertices = modVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        */

        /*Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        espera += Time.deltaTime;
        if(espera > 0.25)
        {
            vertices[indexV].y += 0.71f;
            espera = 0;
            indexV = Random.Range(0, vertices.Length);
            if (indexV > vertices.Length) { indexV = 0; }
        }

        //espera += Time.deltaTime;
        if (espera == 0.25)
        {
            vertices[indexV].y -= 0.21f;
            //espera = 0;
            indexV = Random.Range(0, vertices.Length);
            if (indexV > vertices.Length) { indexV = 0; }
        }
        mesh.vertices = vertices;*/


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Cube")
        {
            Debug.Log("Choca");
        }
    }
}
