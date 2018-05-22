using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Graphmanager : MonoBehaviour {


    public int boarderSize = 10;

    public List<Edge> Edges = new List<Edge>();
    //public List<Vertex> Vertices = new List<Vertex>();

    public Vertex mouseOver;
    //public Vertex mouseOver2;

    public Vertex selected1;
    public Vertex selected2;

    //public GameObject inputField;
    public Text text;



    // Use this for initialization
    void Start () {

        text = GameObject.Find("InputText").GetComponent<Text>();
        //Debug.Log("" + inputText.name);

	}

    public void AddSelected(Vertex ver)
    {

        if (selected1 == null)
        {
            Debug.Log("First vertex...");
            selected1 = ver;
            return;
        }

        if (selected1 != null && mouseOver != selected1)
        {
            Debug.Log("Two joined vertecies!");
            selected2 = ver;

            CreateEdge(selected1, selected2);

            DeSelect(selected1);
            DeSelect(selected2);

            selected1 = null;
            selected2 = null;
        }
    }

    public void RemoveSelected(Vertex ver)
    {
        if(ver == selected1)
        {
            selected1 = null;
        }
        if (ver == selected2)
        {
            selected2 = null;
        }
    }

    GameObject CreateEdge(Vertex v1, Vertex v2)
    {
        GameObject go = Instantiate(Resources.Load("Edge") as GameObject);

        go.GetComponent<Edge>().SetVertices(v1, v2);

        //add edge to the list of edges in each vertex
        v1.Edges.Add(go.GetComponent<Edge>());
        v2.Edges.Add(go.GetComponent<Edge>());


        return go;
    }


    public void CreateVertex()
    {
        if (text.text == "")
        {
            return;
        }

        if (text.text != "")
        {
            ActuallyCreateVertex(text.text);
        }

    }


    GameObject ActuallyCreateVertex(string text)
    {
        GameObject go = Instantiate(Resources.Load("Vertex") as GameObject);
        go.GetComponent<TextMesh>().text = text;
        return go;
    }

    public void Select(Vertex ver)
    {
        Debug.Log("selected");
        ver.selected = true;
        ver.spriteRenderer.color = Color.green;
        AddSelected(ver);
    }

    public void DeSelect(Vertex ver)
    {
        Debug.Log("de selected");
        ver.selected = false;
        ver.ChangeColour();
        RemoveSelected(ver);
    }



    // Update is called once per frame
    void Update () {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 60 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(Vector3.zero, Vector3.down, 60 * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0) && mouseOver != null)
        {
            Select(mouseOver);
        }
        if (Input.GetMouseButtonDown(0) && mouseOver == null && selected1 != null)
        {
            DeSelect(selected1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && selected1 != null)
        {
            Debug.Log("moving up");
            //selected1.Promote();
            selected1.Up();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && selected1 != null)
        {
            Debug.Log("moving down");
            //selected1.Demote();
            selected1.Down();
        }

        if (Input.GetKeyDown(KeyCode.Delete) && selected1 != null)
        {
            Debug.Log("moving down");
            //selected1.Demote();
            selected1.DeleteAttachedEdges();
            selected1.Delete();
        }









    }
}
