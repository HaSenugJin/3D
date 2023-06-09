using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterController : MonoBehaviour
{
    /*
public List<GameObject> PointList;

void Start()
{
    for (int i = 0; i < 72; ++i)
    {
        GameObject obj = new GameObject("point");

        obj.AddComponent<MyGizmo>();

        obj.transform.position = new Vector3(
            Mathf.Sin((i * 5.0f) * Mathf.Deg2Rad),
            Mathf.Cos((i * 5.0f) * Mathf.Deg2Rad),
            0.0f) * 5.0f;

        PointList.Add(obj);
    }



}
    */

    [Range(-90.0f, 90.0f)]
    public float Angle;


    [Range(-10.0f, 20.0f)]
    public float X;

    private void Start()
    {
        gameObject.AddComponent<MyGizmo>();

        X = 5.0f;
        Angle = 0.0f;
    }


    private void Update()
    {
        Vector3 StartPoint = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 EndPoint = new Vector3(10.0f, 5.0f, 0.0f);

        Debug.DrawLine(StartPoint, EndPoint);

        float Width = EndPoint.x - StartPoint.x;
        float Height = EndPoint.y - StartPoint.y;

        //float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        float Y = 0.0f;

        if (StartPoint.x < X && X < EndPoint.x)
            Y = (Height / Width) * X;
        

        Vector3 Movement = new Vector3(X, Y, ver);

        transform.position = Movement;
    }
}
