using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Line
{
    public Vector3 StartPoint;
    public Vector3 EndPoint;
}


public class LineCollision : MonoBehaviour
{
    public List<Line> LineList = new List<Line>();

    [SerializeField] private float Width;
    [SerializeField] private float Height;

    void Start()
    {
        Vector3 OldPoint = new Vector3(0.0f, 0.0f, 0.0f);

        for (int i = 0; i < 20; ++i)
        {
            Line line = new Line();

            line.StartPoint = OldPoint;

            float fY = 0.0f;

            while(true)
            {
                fY = Random.Range(-5.0f, 5.0f);
                if (fY != 0.0f) break;
            }

            OldPoint = new Vector3(
                OldPoint.x + Random.Range(1.0f, 5.0f),
                OldPoint.y + fY,
                0.0f);

            line.EndPoint = OldPoint;

            LineList.Add(line);
        }
        Height = 1.0f;
        Width = 1.0f;
    }

    void Update()
    {
        float Hor = Input.GetAxisRaw("Horizontal");

        if(transform.position.x <= 0.0f)
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        float MovementX = transform.position.x + (Hor * Time.deltaTime * 5.0f);
        Vector3 Offset;

        foreach (Line element in LineList)
        {
            Debug.DrawLine(element.StartPoint, element.EndPoint, Color.green);

            if (element.StartPoint.x <= transform.position.x &&
                transform.position.x < element.EndPoint.x)
            {
                Debug.DrawLine(element.StartPoint, element.EndPoint, Color.red);

                Offset.x = transform.position.x - element.StartPoint.x;
                Offset.y = element.StartPoint.y;
                
                Width = element.EndPoint.x - element.StartPoint.x;
                Height = element.EndPoint.y - element.StartPoint.y;

                transform.position = new Vector3(
                    MovementX,
                    (Height / Width) * Offset.x + Offset.y,
                    0.0f);
            }
        }
    }
}
