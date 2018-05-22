using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class Vertex : MonoBehaviour {


    public Camera mainCam;
    public SpriteRenderer spriteRenderer;
    public TextMesh text;
    public Graphmanager graphmanager;

    public enum Type { Mechanic, Dynamic, Aesthetic };
    public Type type = Type.Mechanic;
   
    public bool selected = false;

    public bool mouseOver = false;

    public List<Edge> Edges = new List<Edge>();


    public Vector3 hitpoint;

	void Start ()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        text = gameObject.GetComponent<TextMesh>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        graphmanager = GameObject.Find("Main Camera").GetComponent<Graphmanager>();
	}

    void OnMouseOver()
    {

        graphmanager.mouseOver = this;

        if (!selected)
        {
            spriteRenderer.color = Color.white;
        }



    }

    void OnMouseExit()
    {
        graphmanager.mouseOver = null;

        if (!selected)
        {
            ChangeColour();
        }


    }

    public void ChangeColour()
    {
        if(type == Type.Mechanic)
        {
            spriteRenderer.color = Color.blue;
        }
        if (type == Type.Dynamic)
        {
            spriteRenderer.color = Color.red;
        }
        if (type == Type.Aesthetic)
        {
            spriteRenderer.color = Color.yellow;
        }
    }

    void OnMouseDrag()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            hitpoint = hit.point;
            Vector3 v = new Vector3(hit.point.x, gameObject.transform.position.y, hit.point.z);
            gameObject.transform.position = v;

        }
    }

    public void Up()
    {
        float f = transform.position.y;
        f += 1;
        transform.position = new Vector3(transform.position.x, f, transform.position.z);
    }

    public void Down()
    {
        float f = transform.position.y;
        f -= 1;
        transform.position = new Vector3(transform.position.x, f, transform.position.z);
    }

    public void Delete()
    {
        GameObject.Destroy(gameObject);
    }

    public void DeleteAttachedEdges()
    {
        for (int i = 0; i < Edges.Count; i++)
        {
            if (Edges[i] != null)
            {
                Edges[i].Delete();
            }
           
                
                
        }

        DeleteRemovedValues();
    }

    public void DeleteRemovedValues()
    {
        for (int i = 0; i < Edges.Count; i++)
        {
            if (Edges[i] = null)
            {
                Edges.RemoveAt(i);
            }

        
        }
    }
    

    public void Promote()
    {
        switch (type)
        {
            case Type.Mechanic:
                ChangeToDynamic();

                break;

            case Type.Dynamic:
                ChangeToAesthetic();
                break;
        }
    }

    public void Demote()
    {
        switch (type)
        {
            case Type.Dynamic:
                ChangeToMechanic();

                break;

            case Type.Aesthetic:
                ChangeToDynamic();
                break;
        }
    }

    public void ChangeToMechanic()
    {
        type = Type.Mechanic;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    void ChangeToDynamic()
    {
        type = Type.Dynamic;
        transform.position = new Vector3(transform.position.x, 3, transform.position.z);
    }

    void ChangeToAesthetic()
    {
        type = Type.Aesthetic;
        transform.position = new Vector3(transform.position.x, 6, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(mainCam.transform);
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);





       

    }
}
