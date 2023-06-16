using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    const int T = 1; // Transform
    const int R = 2; // Rotation
    const int S = 3; // Scale
    const int M = 0; // Matrix

    public Node Target = null;
    public List<GameObject> vertices = new List<GameObject>();
    public List<GameObject> bastList = new List<GameObject>();
    public List<Node> OpenList = new List<Node>();

    private float Speed;

    Vector3 LeftCheck;
    Vector3 RightCheck;

    [Range(0.0f, 180.0f)]
    public float Angle;

    private bool getNode;

    [Range(1.0f, 2.0f)]
    public float scale;


    private GameObject parent;


    private void Awake()
    {
        SphereCollider coll = GetComponent<SphereCollider>();
        coll.radius = 0.05f;
        coll.isTrigger = true;

        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;

        //Target = GameObject.Find("ParentObject").transform.GetChild(0).GetComponent<Node>();
    }

    private void Start()
    {
        parent = new GameObject("Nodes");
        Speed = 5.0f;

        float x = 2.5f;
        float z = 3.5f;

        LeftCheck = transform.position + (new Vector3(-x, 0.0f, z));
        RightCheck = transform.position + (new Vector3(x, 0.0f, z));

        Angle = 45.0f;

        getNode = false;

        scale = 1.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                if(hit.transform.tag != "Node")
                {
                    getNode = true;

                    List<Vector3> zzz = GetVertex(hit.transform.gameObject);
                }

                GameObject startPoint = null;
                float dis = 0.0f;

                float bastDistance = 1000000.0f;

                OpenList.Clear();
                vertices.Clear();

                for (int i = 0; i < temp.Count; ++i)
                {
                    GameObject obj = new GameObject(i.ToString());

                    Matrix4x4[] matrix = new Matrix4x4[4];

                    matrix[T] = Matrix.Translate(hit.transform.position);
                    matrix[R] = Matrix.Rotate(hit.transform.eulerAngles);
                    matrix[S] = Matrix.Scale(hit.transform.lossyScale * scale);

                    matrix[M] = matrix[T] * matrix[R] * matrix[S];

                    Vector3 v = matrix[M].MultiplyPoint(temp[i]);
                    dis = Vector3.Distance(transform.position, v);

                    obj.transform.position = v;
                    obj.AddComponent<Node>();

                    obj.transform.SetParent(parent.transform);
                    MyGizmo gizmo = obj.AddComponent<MyGizmo>();

                    if (dis < bastDistance)
                    {
                        bastDistance = dis;
                        startPoint = obj;

                        if(i == 0)
                            vertices.Add(obj);
                    }
                    else
                        vertices.Add(obj);
                }

                if(startPoint)
                {
                    startPoint.GetComponent<MyGizmo>().color = Color.red;
                    OpenList.Add(startPoint.GetComponent<Node>());
                }

                Node MainNode = OpenList[0].GetComponent<Node>();
                MainNode.Cost = 0.0f;

                while (vertices.Count != 0)
                {
                    float OldDistance = 1000000.0f;
                    int index = 0;

                    for (int i = 0; i < vertices.Count; ++i)
                    {
                        float Distance = Vector3.Distance(OpenList[0].transform.position, vertices[i].transform.position);

                        if (Distance < OldDistance)
                        {
                            OldDistance = Distance;
                            Node Nextnode = vertices[i].GetComponent<Node>();
                            Nextnode.Cost = MainNode.Cost + Distance;
                            index = i;
                        }
                    }

                    if (!OpenList.Contains(vertices[index].GetComponent<Node>()))
                    {
                        Node OldNode = OpenList[OpenList.Count - 1];
                        Node curent = vertices[index].GetComponent<Node>();
                        RaycastHit Hit;

                        if (Physics.Raycast(OldNode.transform.position, curent.transform.position, out Hit, OldDistance))
                        {
                            if (hit.transform.tag != "Node")
                            {
                                
                            }
                            else
                            {

                            }
                        }

                        /*
                         * 조건 2
                         * 이전 노드의 위치에서 EndPoint의 거리보다 현재에서 EndPoint의 거리가 더 짧을때
                         */

                        OpenList.Add(vertices[index].GetComponent<Node>());
                        vertices[index].GetComponent<Node>();
                        
                        vertices.Remove(vertices[index]);
                    }
                }
            }
        }

        List<Vector3> GetVertex(GameObject hitObject)
        {
            List<Vector3> VertexList = new List<Vector3>();

            if (hitObject.transform.childCount != 0)
            {
                for (int i = 0; i < hitObject.transform.childCount; ++i)
                {
                    VertexList.AddRange(
                        GetVertex(hitObject.transform.GetChild(i).gameObject));
                }
            }

            MeshFilter meshFilter = hitObject.GetComponent<MeshFilter>();

            if(meshFilter == null)
            {
                return VertexList;
            }

            Vector3[] verticesPoint = meshFilter.mesh.vertices;

            for (int i = 0; i < verticesPoint.Length; ++i)
            {
                if (!VertexList.Contains(verticesPoint[i])
                    && verticesPoint[i].y < transform.position.y + 0.05f
                    && transform.position.y < verticesPoint[i].y + 0.05f)
                {
                    verticesPoint[i].y = 0.1f;
                    VertexList.Add(verticesPoint[i]);
                }
            }

            return VertexList;
        }

        /*
        if (Target)
        {
            Vector3 Direction = (Target.transform.position - transform.position).normalized;

            transform.rotation = Quaternion.Lerp(
                   transform.rotation,
                   Quaternion.LookRotation(Direction),
                   0.016f);

            if (move)
            {
                transform.position += Direction * Speed * Time.deltaTime;
            }
            else
            {
                Vector3 targetDir = Target.transform.position - transform.position;
                float angle = Vector3.Angle(targetDir, transform.forward);

                if (Vector3.Angle(targetDir, transform.forward) < 0.1f)
                    move = true;
            }
        }
         */
    }


    private void FixedUpdate()
    {
        float startAngle = (transform.eulerAngles.y - Angle);

        RaycastHit hit;

        Debug.DrawRay(transform.position,
            new Vector3(
                Mathf.Sin(startAngle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(startAngle * Mathf.Deg2Rad)) * 2.5f,
            Color.white);

        if (Physics.Raycast(transform.position, LeftCheck, out hit, 5.0f))
        {

        }

        Debug.DrawRay(transform.position,
             new Vector3(
                 Mathf.Sin((transform.eulerAngles.y + Angle) * Mathf.Deg2Rad), 0.0f, Mathf.Cos((transform.eulerAngles.y + Angle) * Mathf.Deg2Rad)) * 2.5f,
             Color.green);

        if (Physics.Raycast(transform.position, RightCheck, out hit, 5.0f))
        {

        }

        //int Count = (int)((Angle * 2) / 5.0f);

        for (float f = startAngle + 5.0f; f < (transform.eulerAngles.y + Angle - 5.0f); f += 5.0f)
        {
            Debug.DrawRay(transform.position,
                new Vector3(
                    Mathf.Sin(f * Mathf.Deg2Rad), 0.0f, Mathf.Cos(f * Mathf.Deg2Rad)) * 2.5f,
                Color.red);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        getNode = false;
        
        /*
        if (Target.transform.name == other.transform.name)
            Target = Target.Next;
         */
    }


    void Outpot(Matrix4x4 _m)
    {
        Debug.Log("==============================================");
        Debug.Log(_m.m00 + ", " + _m.m01 + ", " + _m.m02 + ", " + _m.m03);
        Debug.Log(_m.m10 + ", " + _m.m11 + ", " + _m.m12 + ", " + _m.m13);
        Debug.Log(_m.m20 + ", " + _m.m21 + ", " + _m.m22 + ", " + _m.m23);
        Debug.Log(_m.m30 + ", " + _m.m31 + ", " + _m.m32 + ", " + _m.m33);
    }
}
