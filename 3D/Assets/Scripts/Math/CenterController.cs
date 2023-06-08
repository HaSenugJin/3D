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

    private void Start()
    {
        gameObject.AddComponent<MyGizmo>();

        Angle = 0.0f;
    }


    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");


        Vector3 Movement = new Vector3(hor, Mathf.Sin(Angle * Mathf.Deg2Rad) * 5.0f, ver) * 5.0f * Time.deltaTime;

        transform.Translate(Movement);
    }
}
