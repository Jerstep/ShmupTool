using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeRay : MonoBehaviour {
    // Gameobjects
    public GameObject enemyPrefab;
    public GameObject shape;

    public int enemyAmount;

    public float radius;
    private float angle;

    private Vector3 position;

    [SerializeField] private List<Vector3> hitPoints;

    private Vector3 temp = new Vector3();
    private LayerMask layerMask;

    // Use this for initialization
    void Awake ()
    {
        position = this.transform.position;
        layerMask = 1 << 8;
        CreateShape();
    }

    void Update()
    {
        if(transform.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    void CreateShape()
    {
        GameObject shapeObject = Instantiate(shape, position, shape.transform.rotation);
        shapeObject.transform.localScale *= radius;
        SetPositions();
        DestroyImmediate(shapeObject);             
    }


    void SetPositions()
    {
        Quaternion quaternion = Quaternion.AngleAxis(360f / (float)(enemyAmount), transform.up);
        Vector3 vec3 = transform.forward * radius;

        RaycastHit hit;

        for(int i = 0; i < enemyAmount; i++)
        {
            if(Physics.Raycast(transform.position, vec3, out hit, Mathf.Infinity, layerMask))
            {
                if(hit.collider.CompareTag("shape"))
                {
                    temp = hit.point;

                    hitPoints.Add(temp);
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    //Vector3 hitPos = hit.point;

                    vec3 = quaternion * vec3;
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
        InstantiateEnemies();        
    }

    void InstantiateEnemies()
    {
        for (int i = 0; i < hitPoints.Count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, hitPoints[i], enemyPrefab.transform.rotation) as GameObject;
            enemy.transform.parent = this.transform;
        }
    }
}
