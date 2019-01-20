using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private GameObject middleObject;
    public float speed;
    private float randomRotation;

	// Use this for initialization
	void Start ()
    {
        randomRotation = Random.Range(0.0f, 360.0f);
        //this.gameObject.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }

    void OrbitAround()
    {
        //transform.RotateAround(middleObject.transform.position, Vector3.forward, speed );
    }
}
