using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    // Start is called before the first frame update

    public CircleCollider2D objectCollider;
    public Collider2D anotherCollider;

    
    void Start()
    {
        objectCollider = transform.GetComponent<CircleCollider2D>();
        anotherCollider = GameObject.Find("Player").GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
	
    }

    
    void OnCollisionEnter2D(Collision2D other) {
	Debug.Log("h");
	if (other.gameObject.tag == "Player") {
	    Debug.Log("works");
	    other.gameObject.GetComponent<PlayerHealth>().TakeDamage();
	}
	
    }
}
