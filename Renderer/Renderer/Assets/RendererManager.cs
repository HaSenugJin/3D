using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererManager : MonoBehaviour
{
    public Renderer renderer;

    private const string path = "Legacy Shaders/Transparent/Specular";

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    IEnumerator SetColor(Renderer renderer)
    {
        Material material = new Material(Shader.Find(path));
        Color color = renderer.material.color;

        while (0.5f < color.a)
        {
            yield return null;

            color.a -= Time.deltaTime;
            renderer.material.color = color;
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            if (renderer != null)
            {
                StartCoroutine(SetColor(renderer));
            }
        }
    }
}
