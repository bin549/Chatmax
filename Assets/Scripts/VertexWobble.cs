using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VertexWobble : MonoBehaviour
{
    private TMP_Text textMesh;
    private Mesh mesh;
    private Vector3[] vertices;
    [SerializeField] private float wobbleX = 3.3f;
    [SerializeField] private float wobbleY = 2.5f;

    private void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);
            vertices[i] = vertices[i] + offset;
        }
        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    private Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * wobbleX), Mathf.Cos(time * wobbleX));
    }
}