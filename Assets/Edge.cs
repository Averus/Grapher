using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Edge : MonoBehaviour {

    public Vertex v1;
    public Vertex v2;

    LineRenderer lr;

    void Awake()
    {
        lr = gameObject.GetComponent<LineRenderer>();
    }


    public void SetVertices(Vertex v1, Vertex v2)
    {
        this.v1 = v1;
        this.v2 = v2;
    }

    public void Delete()
    {
        GameObject.Destroy(gameObject);
    }


    void Update()
    {
        lr.SetPosition(0, v1.transform.position);
        lr.SetPosition(1, v2.transform.position);
    }

}
