using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private GameObject perent;

    void Start()
    {
        perent = GameObject.Find("ParentObject");
    }

    void Update()
    {
        for(int i =0;i<perent.transform.childCount; ++i)
        {
            Node node = perent.transform.GetChild(i).GetComponent<Node>();

            //Debug.DrawLine(node.transform.position, node.Next.transform.position);
        }
    }
}
