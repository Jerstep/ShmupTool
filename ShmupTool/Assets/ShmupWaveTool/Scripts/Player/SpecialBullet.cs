using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet : MonoBehaviour
{


    [SerializeField] private float speed;
    private Rigidbody RigidBullet;

    [Header("Particle on collision")]
    public GameObject hitParticle;

    public bool p1Owner;
    // Use this for initialization
    void Start()
    {
        RigidBullet = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(p1Owner)
        {
            RigidBullet.velocity = transform.up * -speed;
        }
        else
        {
            RigidBullet.velocity = transform.up * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player-1" && !p1Owner)
        {
           
            other.GetComponent<Player>().playerHealth -= 20;
            
            Instantiate(hitParticle, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if(other.tag == "Player-2" && p1Owner)
        {
            
            other.GetComponent<Player>().playerHealth -= 20;
            
            Instantiate(hitParticle, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if(other.tag == "Walls" || other.tag == "WallDeath")
        {
            Destroy(gameObject);
        }
    }
}