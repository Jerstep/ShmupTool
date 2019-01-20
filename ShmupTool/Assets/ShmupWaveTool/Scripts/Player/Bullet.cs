using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private float speed;
    private Rigidbody RigidBullet;

    [Header("Bullet Speeds")]
    public int heavySpeed;
    public int normalSpeed;

    [Header("Particle on collision")]
    public GameObject hitParticle;

    [HideInInspector] public bool heavy;
    [HideInInspector] public bool p1Owner, enemyOwner;
    // Use this for initialization
    void Start ()
    {
        if (heavy)
        {
            speed = heavySpeed;
        }
        else
        {
            speed = normalSpeed;
        }
        RigidBullet = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (p1Owner || enemyOwner)
        {
            RigidBullet.velocity = transform.up * speed;
        }
        else
        {
            RigidBullet.velocity = transform.up * -speed;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player-1")
        {
            
                other.GetComponent<Player>().playerHealth -= 10;
            Instantiate(hitParticle, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (other.tag == "Player-2")
        {
            
                other.GetComponent<Player>().playerHealth -= 10;
            
            Instantiate(hitParticle, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (other.tag == "Enemy" && enemyOwner == false)
         {
            other.GetComponentInParent<Enemy>().enemyHp -= 10;

            if (!heavy)
            {
                Instantiate(hitParticle, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }              
        }

        if (other.tag == "Boss" && enemyOwner == false)
        {
            other.GetComponentInParent<Boss>().bossHp -= 1;
            Instantiate(hitParticle, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (other.CompareTag("Walls") || other.CompareTag("WallDeath") || other.CompareTag("SpecialBullet"))
        {
            Destroy(gameObject);
        }
    }
}
