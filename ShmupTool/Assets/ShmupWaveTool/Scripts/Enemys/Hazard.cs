using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

    private Rigidbody RigidEnemy;

    public float moveSpeedX, moveSpeedY;
    public bool patternr;

    public int switchTime;

    // Use this for initialization
    void Start ()
    {
        RigidEnemy = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        RigidEnemy.velocity = new Vector3(RigidEnemy.velocity.x, moveSpeedY * 3, 0);
    }
}
