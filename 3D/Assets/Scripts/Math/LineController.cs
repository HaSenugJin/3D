using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public Vector3 StartPoint;
    public Vector3 EndPoint;
}

public class LineController : MonoBehaviour
{
    public List<Line> LineList = new List<Line>();

    [SerializeReference] private float Width;
    [SerializeReference] private float Height;

    void Start()
    {
        Vector3 OldPoint = new Vector3(0.0f, 0.0f, 0.0f);

        for (int i = 0; i < 20; ++i)
        {
            Line line = new Line();

            line.StartPoint = OldPoint;

            OldPoint = new Vector3(OldPoint.x + Random.Range(1.0f, 5.0f), OldPoint.y + Random.Range(-5.0f, 5.0f), 0.0f);

            line.EndPoint = OldPoint;

            LineList.Add(line);
        }
        Height = 1.0f;
        Width = 0.0f;
    }

    void Update()
    {
        foreach (Line element in LineList)
        {
            Debug.DrawLine(element.StartPoint, element.EndPoint, Color.green);

            //Width = EndPoint.x - StartPoint.x;
            //Height = EndPoint.y - StartPoint.y;
        }

        transform.position = new Vector3(
            transform.position.x,
            (Height / Width) * (transform.position.x),
            0.0f);
    }
}