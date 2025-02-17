﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTexture : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh mesh;
    Vector2[] uvs;
    // Use this for initialization
    void Awake()
    {
        // meshFilter = GetComponent<MeshFilter>();
        // Mesh meshcopy = Mesh.Instantiate(meshFilter.sharedMesh) as Mesh;
        // meshcopy.name = "Cube2";
        // mesh = meshFilter.mesh = meshcopy;
        // if (mesh == null || mesh.uv.Length != 24)
        // {
        //     Debug.Log("Script needs to be attached to built-in cube");
        //     return;
        // }
        // uvs = mesh.uv;
        // uvs[0] = new Vector2(0.0f, 0.5f);
        // uvs[1] = new Vector2(0.0f, 0.0f);
        // uvs[2] = new Vector2(0.5f, 0.5f);
        // uvs[3] = new Vector2(0.5f, 0.0f);
        // for (int i = 1; i < 6; i++)
        // {
        //     uvs[i * 4 + 0] = new Vector2(0.0f, 0.5f);
        //     uvs[i * 4 + 1] = new Vector2(0.0f, 0.0f);
        //     uvs[i * 4 + 2] = new Vector2(0.5f, 0.5f);
        //     uvs[i * 4 + 3] = new Vector2(0.5f, 0.0f);
        //     // uvs[i * 4] = new Vector2(0.0f, 0.5f);
        //     // uvs[i * 4 + 1] = new Vector2(1.0f, 0.5f);
        //     // uvs[i * 4 + 2] = new Vector2(0.0f, 1.0f);
        //     // uvs[i * 4 + 3] = new Vector2(1.0f, 1.0f);
        // }
        // mesh.uv = uvs;
    }
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        Mesh meshcopy = Mesh.Instantiate(meshFilter.sharedMesh) as Mesh;
        meshcopy.name = "Cube2";
        mesh = meshFilter.mesh = meshcopy;
        if (mesh == null || mesh.uv.Length != 24)
        {
            Debug.Log("Script needs to be attached to built-in cube");
            return;
        }
        uvs = mesh.uv;

        // Front
        uvs[0] = new Vector2(1.0f, 1.0f);
        uvs[1] = new Vector2(0.5f, 1.0f);
        uvs[2] = new Vector2(1.0f, 0.0f);
        uvs[3] = new Vector2(0.5f, 0.0f);

        // Top
        uvs[8] = new Vector2(1.0f, 1.0f);
        uvs[9] = new Vector2(0.5f, 1.0f);
        uvs[4] = new Vector2(1.0f, 0.0f);
        uvs[5] = new Vector2(0.5f, 0.0f);

        // Back
        uvs[10] = new Vector2(0.5f, 1.0f);
        uvs[11] = new Vector2(0.0f, 1.0f);
        uvs[6] = new Vector2(0.5f, 0.0f);
        uvs[7] = new Vector2(0.0f, 0.0f);

        // Bottom
        uvs[15] = new Vector2(1.0f, 1.0f);
        uvs[14] = new Vector2(0.5f, 1.0f);
        uvs[12] = new Vector2(1.0f, 0.0f);
        uvs[13] = new Vector2(0.5f, 0.0f);

        // Left
        uvs[19] = new Vector2(1.0f, 1.0f);
        uvs[18] = new Vector2(0.5f, 1.0f);
        uvs[16] = new Vector2(1.0f, 0.0f);
        uvs[17] = new Vector2(0.5f, 0.0f);

        // Right
        uvs[23] = new Vector2(1.0f, 1.0f);
        uvs[22] = new Vector2(0.5f, 1.0f);
        uvs[20] = new Vector2(1.0f, 0.0f);
        uvs[21] = new Vector2(0.5f, 0.0f);

        mesh.uv = uvs;

    }

    // Update is called once per frame
    void Update()
    {
    }
}
