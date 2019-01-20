using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player-1" || other.tag == "Player-2")
        {
            other.GetComponent<Player>().playerHealth -= 10;
            Destroy(gameObject);
        }
        if (other.tag == "WallDeath")
        {
            Destroy(gameObject);
        }
    }
}
