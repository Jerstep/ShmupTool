using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody RigidBoss;
    public float bossHp;
    public bool goingUp;
    public float moveSpeed;

    // Use this for initialization
    void Start () {
        RigidBoss = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		if(bossHp <= 0)
        {
            Destroy(gameObject);
        }

        if(goingUp)
        {
            RigidBoss.velocity = new Vector3(0, moveSpeed, 0);
        }
        else
        {
            RigidBoss.velocity = new Vector3(0, -moveSpeed, 0);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Walls")
        {
            goingUp = !goingUp;
        }
    }
}
