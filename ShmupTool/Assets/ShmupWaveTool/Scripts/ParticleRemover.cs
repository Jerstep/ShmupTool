using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRemover : MonoBehaviour {

    public float timeTillDeath;

	// Use this for initialization
	void Start () {
        StartCoroutine(Die());
	}
	

    IEnumerator Die()
    {
        yield return new WaitForSeconds(timeTillDeath);
        Destroy(gameObject);
    }
}
