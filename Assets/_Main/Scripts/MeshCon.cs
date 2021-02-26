using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCon : MonoBehaviour
{
    private Mesh mesh;
    Vector3[] modVertices;
    MeshCollider MeshCol;
    public float perlNoisVal;
    public float power;
    public float SinVal;
    public float espera = 0f;
    public float deformation = 2f;
    public Transform other;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        modVertices = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        espera += Time.deltaTime;
        if (espera > 0.1f)
        {
            perlNoisVal += Time.deltaTime;
            espera = 0;
            if(perlNoisVal >= 1)
            {
                perlNoisVal = 0;
            }
        }
        

        for (int i = 0; i < modVertices.Length; i++)
        {
            modVertices[i] = new Vector3(modVertices[i].x, Mathf.PerlinNoise(modVertices[i].x * perlNoisVal, modVertices[i].z * perlNoisVal) * power, modVertices[i].z);
            //modVertices[i].y = Mathf.Sin(SinVal * i + Time.deltaTime);
            //modVertices[i] = modVertices[i] + (Vector3.up * 0.5f) / 0.34f;
            //float y = Mathf.PerlinNoise(0.3f * modVertices.Length, 0.3f * modVertices.Length) * 2f;
            //modVertices[i] = new Vector3(modVertices[i].x, y, modVertices[i].z);
        }
        

        mesh.vertices = modVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

    }
}    
    
