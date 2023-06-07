#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : EditorWindow
{
    [MenuItem("CustomEditors/WayPoint")]
    public static void ShowWindows()
    {
        GetWindow<WayPointEditor>("WayPoint");
    }

    private GameObject Parent;

    private void OnGUI()
    {
        Parent = (GameObject)EditorGUILayout.ObjectField("Parent", Parent, typeof(GameObject),true);

        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Add Node", GUILayout.Width(350), GUILayout.Height(25)))
            {
                if(Parent != null)
                {
                    GameObject NodeObject = new GameObject("Node");
                    Node node = NodeObject.AddComponent<Node>();

                    //node¿« º≥¡§

                    NodeObject.transform.SetParent(Parent.transform);
                    NodeObject.transform.transform.position = new Vector3(
                        Random.Range(-10.0f,10.0f),0.0f, Random.Range(-10.0f, 10.0f));
                }
            }

            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
    }
}
#endif