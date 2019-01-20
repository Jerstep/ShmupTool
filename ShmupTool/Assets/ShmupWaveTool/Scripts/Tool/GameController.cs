using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject player1, player2;

	// Use this for initialization
	void Awake ()
    {
        player1 = GameObject.FindGameObjectWithTag("Player-1");
        player2 = GameObject.FindGameObjectWithTag("Player-2");        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
