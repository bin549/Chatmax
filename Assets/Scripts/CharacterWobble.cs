using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterWobble : MonoBehaviour
{
    private TMP_Text textMesh;
    private Mesh mesh;
    private Vector3[] vertices;
    [SerializeField] private float wobbleX = 3.3f;
    [SerializeField] private float wobbleY = 2.5f;
    [SerializeField] private float defaltWobbleX;
    [SerializeField] private float defaltWobbleY;
    [SerializeField] private float defaltTextSize;

    private void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
        defaltTextSize = textMesh.fontSize;
    }

    private void Start()
    {
        defaltWobbleX = wobbleX;
        defaltWobbleY = wobbleY;
    }

    private void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;
        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];
            int index = c.vertexIndex;
            Vector3 offset = Wobble(Time.time + i);
            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;
        }
        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    private Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * wobbleX), Mathf.Cos(time * wobbleY));
    }

    public void SetDefault()
    {
        this.wobbleX = this.defaltWobbleX;
        this.wobbleY = this.defaltWobbleY;
        textMesh.fontSize = defaltTextSize;
    }

    public void SetScary()
    {
        this.wobbleX = 500;
        this.wobbleY = 500;
        textMesh.fontSize = defaltTextSize * 1.2f;
    }
}