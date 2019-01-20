using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public float enemyHp;
    public GameObject deathParticle;
    public GameObject explotionCollider;
    public GameObject powerupPrefab;

    [Header("Droprate Powerup's")]
    public int dropChanceMin = 1;
    public int dropChanceMax = 1;
    public int dropInt = 1;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHp <= 0)
        {
            Kill();
        }

    }

    void Kill()
    {
        Instantiate(deathParticle, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(explotionCollider, gameObject.transform.position, gameObject.transform.rotation);

        GameObject tempPrefab = powerupPrefab;
        if(powerupPrefab != null)
        {
            if(Random.Range(dropChanceMin, dropChanceMax) == dropInt)
            {
                Instantiate(tempPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
        Destroy(gameObject);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WallDeath") || other.CompareTag("Hazard") || other.CompareTag("Boss") || other.CompareTag("SpecialBullet"))
        {
            Kill();
        }
    }
}
